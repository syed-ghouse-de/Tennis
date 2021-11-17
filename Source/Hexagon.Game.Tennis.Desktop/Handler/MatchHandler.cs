﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hexagon.Game.Framework;
using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Extension;
using Hexagon.Game.Tennis.Desktop.Model;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis.Desktop.Handler
{
    /// <summary>
    /// Match handler for observable
    /// </summary>
    public class MatchHandler : IMatchHandler
    {
        // Observable & Subject objects 
        private readonly CompositeDisposable _disposable;
        private readonly IConnectableObservable<ScoreModel> _scoreObservable;
        private Subject<ScoreModel> _scoreSubject = new Subject<ScoreModel>();

        private readonly object _lock;
        private bool _scoreConnected;

        // Handler & Match member variables
        private static IMatchHandler _instance;
        private IMatch _match;

        /// <summary>
        /// Instance of an match handler
        /// </summary>
        public static IMatchHandler Instance
        {
            get
            {
                // Create a single instance of a handler
                if (_instance == null)
                    _instance = new MatchHandler();
                return _instance;
            }
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private MatchHandler()
        {
            _lock = new object();
            _disposable = new CompositeDisposable();
            _scoreObservable = _scoreSubject.ObserveOn(Scheduler.Default).Publish();
        }

        /// <summary>
        /// 
        /// </summary>
        public IMatch Match { get { return _match; } set { _match = value; } }

        /// <summary>
        /// Observable for Score
        /// </summary>
        public IObservable<ScoreModel> Score
        {
            get
            {
                // Connect score observable
                ConnectScoreObservable();                   
                return _scoreSubject.DistinctUntilChanged();
            }
        }

        /// <summary>
        /// Score observable connection
        /// </summary>
        private void ConnectScoreObservable()
        {
            if (_scoreConnected)
                return;

            // Lock for to connect score observable for multiple request
            lock(_lock)
            {
                if (_scoreConnected)
                    return;

                // Connect score observable
                var disposable = _scoreObservable.Connect();
                _disposable.Add(disposable);

                _scoreConnected = true;
            }
        }

        /// <summary>
        /// Start a match
        /// </summary>
        public void Start()
        {
            try
            {               
                Task.Run(() =>
                    {
                        _match.Start();

                        Random rnd = new Random();

                        while (true)
                        {
                            int winner = rnd.Next() % 2;
                            int looser = (winner + 1) % 2;

                            if (winner == 1)
                                _match.Players.FirstPlayer.Win();
                            else
                                _match.Players.SecondPlayer.Win();

                            Thread.Sleep(2000);
                        }
                    }
                );
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Configure math details before starting the match
        /// </summary>
        /// <param name="match">Match to start</param>
        public void Initialize(MatchEntity match)
        {
            _match = new Match();

            if (!match.Players.Any())
                throw new InvalidOperationException("Playres needs to be initialized before starting the match");

            _match.Players.Add(new Player(match.Players[0]));
            _match.Players.Add(new Player(match.Players[1]));

            _match.ScoreUpdate += OnScoreUpdate;
            _match.MatchWin += OnMatchWin;

            var firstPlayer = _match.Players.FirstPlayer;
            var secondPlayer = _match.Players.SecondPlayer;
        }

        /// <summary>
        /// Action event for match win
        /// </summary>
        /// <param name="winner">Winner of a match</param>
        /// <param name="score">Score of a player</param>
        private void OnMatchWin(PlayerEntity winner, ScoreEntity score)
        {
            try
            {
                // Prepare Score model
                ScoreModel model = GetScore(score);

                // Notify for all the subscribers            
                _scoreSubject.OnNext(model);
            }
            catch(Exception) { }
        }

        public ScoreModel GetScore(ScoreEntity score)
        {
            ScoreModel model = new ScoreModel();

            model.Match = new MatchModel()
            {
                Name = _match.Name,
                Date = _match.StartedOn          
            };

            PlayerModel firstPlayer = new PlayerModel()
            {
                Id = _match.Players.FirstPlayer.Identity.Id,
                FirstName = _match.Players.FirstPlayer.Identity.FirstName,
                SurName = _match.Players.FirstPlayer.Identity.SurName,
                LastName = _match.Players.FirstPlayer.Identity.LastName,
                Sets = new List<string>(),
                Point = _match.Players.FirstPlayer.Point.Point.ToDigit()
            };
            firstPlayer.GamesWon = score.Sets.ToDictionary(k => k.Id, k => k.Games.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Id)).Count());
            firstPlayer.Sets = score.Sets.Select(k => k.Games.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Id)).Count().ToString()).ToList();

            PlayerModel secondPlayer = new PlayerModel()
            {
                Id = _match.Players.SecondPlayer.Identity.Id,
                FirstName = _match.Players.SecondPlayer.Identity.FirstName,
                SurName = _match.Players.SecondPlayer.Identity.SurName,
                LastName = _match.Players.SecondPlayer.Identity.LastName,
                Sets = new List<string>(),
                Point = _match.Players.SecondPlayer.Point.Point.ToDigit()
            };
            secondPlayer.GamesWon = score.Sets.ToDictionary(k => k.Id, k => k.Games.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Id)).Count());
            secondPlayer.Sets = score.Sets.Select(k => k.Games.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Id)).Count().ToString()).ToList();

            for(int count = 0; count < _match.BestOfSets - firstPlayer.Sets.Count; count ++)
            {
                firstPlayer.Sets.Add("");
                secondPlayer.Sets.Add("");
            }

            model.Players.Add(firstPlayer);
            model.Players.Add(secondPlayer);

            model.Games = score.Sets.ToDictionary(k => k.Id, v => v.Games.Select(g => new GameModel()
            {
                Server = new PlayerModel()
                {
                    Id = g.Server.Id,
                    FirstName = g.Server.FirstName,
                    SurName = g.Server.SurName,
                    Point = String.Format("{0}: {1}", "S", string.Join(" ", 
                        g.PlayerPoints.Where(p => p.Player.Id.Equals(g.Server.Id)).Select(s => s.Point.ToDigit())))
                },
                Receiver = new PlayerModel()
                {
                    Point = String.Format("{0}: {1}", "R", string.Join(" ", 
                        g.PlayerPoints.Where(p => !p.Player.Id.Equals(g.Server.Id)).Select(s => s.Point.ToDigit())))
                }
            }).ToList());

            return model;
        }

        /// <summary>
        /// Action even for score update
        /// </summary>
        /// <param name="winner">Winner of the player</param>
        /// <param name="score">Score of the player</param>
        private void OnScoreUpdate(PlayerEntity winner, ScoreEntity score)
        {
            try
            {
                // Prepare Score model
                ScoreModel model = GetScore(score);
             
                // Notify for all the subscribers            
                _scoreSubject.OnNext(model);
            }
            catch (Exception) { }
        }
    }
}
