using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IConnectableObservable<ScoreEntity> _scoreObservable;
        private Subject<ScoreEntity> _scoreSubject = new Subject<ScoreEntity>();

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
            _match = new Match();

            _match.ScoreUpdate += OnScoreUpdate;
            _match.MatchWin += OnMatchWin;

            _lock = new object();
            _disposable = new CompositeDisposable();
            _scoreObservable = _scoreSubject.ObserveOn(Scheduler.Default).Publish();
        }

        /// <summary>
        /// Observable for Score
        /// </summary>
        public IObservable<ScoreEntity> Score
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
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            _match.Players = players;
            _match.Players.Server = first;
            var f = _match.Players.FirstPlayer;
            var s = _match.Players.SecondPlayer;

            _match.Start();
            _match.Players.FirstPlayer.Win();
        }

        /// <summary>
        /// Action event for match win
        /// </summary>
        /// <param name="winner">Winner of a match</param>
        /// <param name="score">Score of a player</param>
        private void OnMatchWin(PlayerEntity winner, ScoreEntity score)
        {
            // Notify for all the subscribers
            _scoreSubject.OnNext(_match.Score);
        }
        
        /// <summary>
        /// Action even for score update
        /// </summary>
        /// <param name="winner">Winner of the player</param>
        /// <param name="score">Score of the player</param>
        private void OnScoreUpdate(PlayerEntity winner, ScoreEntity score)
        {
            // Nofity for all the subscribers
            _scoreSubject.OnNext(_match.Score);
        }
    }
}
