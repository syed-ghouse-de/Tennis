using Autofac;
using System;
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
using Hexagon.Game.Framework.DependencyInjection;
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
                    _instance = new MatchHandler(null);
                return _instance;
            }
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        public MatchHandler(Match match)
        {
            _match = match;

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
            lock (_lock)
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
                // Execute the player wins Asynchronously
                Task.Run(() =>
                    {
                        // Start the match
                        _match.Start();
                        Random rnd = new Random();

                        // Continue the play till the match is not completed
                        while (!_match.Status.Equals(Status.Completed))
                        {
                            // Get the random number to make the player win
                            int player = rnd.Next() % 2;                            

                            if (player == 1)
                                _match.Players.FirstPlayer.Win();
                            else
                                _match.Players.SecondPlayer.Win();

                            // Sleep for 2 seconds for delay between players wins
                            Thread.Sleep(2000);
                        }
                    }
                );
            }
            catch (Exception exception)
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
            // Throw exception when no palyers are added
            if (!match.Players.Any())
                throw new InvalidOperationException("Playres needs to be initialized before starting the match");

            // Initialize players details            
            _match.NewMatch(match);            
            _match.Players.Add(new Player(match.Players[0]));
            _match.Players.Add(new Player(match.Players[1]));

            // Subscribe delegates for both score update and match win
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

        /// <summary>
        /// Prepare the score mobel for score board
        /// </summary>
        /// <param name="score">Score of the match</param>
        /// <returns>Return match score</returns>
        public ScoreModel GetScore(ScoreEntity score)
        {
            // Lock an object for sync before preparing the score 
            lock (_lock)
            {
                ScoreModel model = new ScoreModel();

                // Prepare match model
                model.Match = new MatchModel()
                {
                    Name = _match.Name,
                    StartedOn = _match.StartedOn,
                    BestOfSets = _match.BestOfSets,
                    Status = _match.Status
                };

                // Initialize match won player details if match is completed
                if (Match.WonBy != null && Match.Status.Equals(Status.Completed))
                {
                    // When the match is completed and by whom
                    model.Match.CompletedOn = _match.CompletedOn;                    
                    model.Match.WonBy = new PlayerModel()
                    {
                        Id = Match.WonBy.Id,
                        FirstName = Match.WonBy.FirstName,
                        SurName = Match.WonBy.SurName,
                        LastName = Match.WonBy.LastName
                    };
                }

                // Prepare first player model
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
                firstPlayer.Server = _match.Players.Server.Identity.Id.Equals(firstPlayer.Id) ? "*" : string.Empty;

                // Prepare Second player model
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
                secondPlayer.Server = _match.Players.Server.Identity.Id.Equals(secondPlayer.Id) ? "*" : string.Empty;

                // Add empty for the remaining Sets
                int emptySets = _match.BestOfSets - firstPlayer.Sets.Count;  
                for (int count = 0; count < emptySets; count++)
                {
                    firstPlayer.Sets.Add("");
                    secondPlayer.Sets.Add("");
                }

                // Add player to the model
                model.Players.Add(firstPlayer);
                model.Players.Add(secondPlayer);

                // Prepare Set list
                for (int setCount = 1; setCount <= score.Sets.Count; setCount++)
                    model.Sets.Add(score.Sets[setCount - 1].Id, string.Format("Set {0}", setCount));

                // Prepare games list for referee board
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
