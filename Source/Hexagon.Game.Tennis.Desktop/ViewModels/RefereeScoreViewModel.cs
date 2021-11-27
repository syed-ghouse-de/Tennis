using Hexagon.Game.Framework.MVVM.Command;
using Hexagon.Game.Tennis.Desktop.Handler;
using Hexagon.Game.Tennis.Desktop.Model;
using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hexagon.Game.Tennis.Desktop.ViewModels
{
    /// <summary>
    /// Referee score view model
    /// </summary>
    public class RefereeScoreViewModel : BaseViewModel, IDisposable
    {
        private IMatchHandler _matchHandler;
        private readonly CompositeDisposable _disposable;       

        /// <summary>
        /// Default constructor
        /// </summary>
        public RefereeScoreViewModel(IMatchHandler handler)
        {            
            // Initialize the match handler
            _matchHandler = handler;

            // Subscribe for score change 
            _disposable = new CompositeDisposable
            {
                // Subscribe for score 
                _matchHandler.Score
                    .Subscribe(s =>
                    {
                        Score = s;
                    }, e =>
                    {
                        Score = new ScoreModel();
                    })
            };

            // Get the players from the database
            Players = _matchHandler.Match.GetPlayers(); 
        }

        /// <summary>
        /// Match Handler instance
        /// </summary>
        public IMatchHandler MatachHandler { get { return _matchHandler; } }

        /// <summary>
        /// Match model 
        /// </summary>
        private MatchModel _match = new MatchModel();
        public MatchModel Match
        {
            get { return _match; }
            set
            {
                SetPropertyAndNotify(ref _match, value, () => Match);
            }
        }

        /// <summary>
        /// Match updated score
        /// </summary>
        private ScoreModel _score;
        public ScoreModel Score
        {
            get { return _score; }
            set
            {
                // Set and notifiy the change of Score data
                SetPropertyAndNotify(ref _score, value, () => Score);
                IsMatchInProgress = Score.Match.Status.Equals(Status.InProgress);               // Check if match is in progress
                Message = string.Empty;

                // If selected Set is empty or it has only one Set, the refreh the Game data
                if (_selectedSet.Key.Equals(Guid.Empty) || _selectedSet.Key.Equals(Score.Sets.LastOrDefault().Key))                
                    SelectedSet = value.Sets.LastOrDefault();
            }
        }

        /// <summary>
        /// Selected Set
        /// </summary>
        private KeyValuePair<Guid, string> _selectedSet = new KeyValuePair<Guid, string>();
        public KeyValuePair<Guid, string> SelectedSet
        {
            get { return _selectedSet; }
            set
            {
                // Set and notify the Selected Set
                SetPropertyAndNotify(ref _selectedSet, value, () => SelectedSet);

                // If any one is NULL, then do not continue
                if (_score == null || _selectedSet.Key.Equals(Guid.Empty))
                    return;

                // Add default values to both the players
                GamesWon = new List<int>()
                {
                    Score.Players[0].GamesWon[SelectedSet.Key],
                    Score.Players[1].GamesWon[SelectedSet.Key]
                };         
                Games = Score.Games[SelectedSet.Key];
            }
        }

        /// <summary>
        /// Games data
        /// </summary>
        private List<GameModel> _games = new List<GameModel>();
        public List<GameModel> Games
        {
            get { return _games; }
            set { SetPropertyAndNotify(ref _games, value, () => Games); }
        }

        /// <summary>
        /// Store the list available players
        /// </summary>
        private List<PlayerEntity> _players = new List<PlayerEntity>();
        public List<PlayerEntity> Players
        {
            get { return _players; }
            set
            {
                SetPropertyAndNotify(ref _players, value, () => Players);
               
                // Assigne the list of players to Player 1 and 2
                if (Players.Any())
                {
                    FirstPlayer = value.FirstOrDefault();
                    SecondPlayer = value.FirstOrDefault();
                }                    
            }
        }

        /// <summary>
        /// Selected player one 
        /// </summary>
        private PlayerEntity _playerOne = new PlayerEntity();
        public PlayerEntity FirstPlayer
        {
            get { return _playerOne; }
            set
            {
                // Set and notify the selced player one
                SetPropertyAndNotify(ref _playerOne, value, () => FirstPlayer);
            }
        }

        /// <summary>
        /// Selected player one 
        /// </summary>
        private PlayerEntity _playerTwo = new PlayerEntity();
        public PlayerEntity SecondPlayer
        {
            get { return _playerTwo; }
            set
            {
                // Set and notify the selced player one
                SetPropertyAndNotify(ref _playerTwo, value, () => SecondPlayer);
            }
        }

        /// <summary>
        /// Games Wons
        /// </summary>
        private List<int> _gamesWon = new List<int>();
        public List<int> GamesWon
        {
            get { return _gamesWon; }
            set { SetPropertyAndNotify(ref _gamesWon, value, () => GamesWon); }
        }

        /// <summary>
        /// Match progess
        /// </summary>
        private bool _isMatchInProgress = false;
        public bool IsMatchInProgress
        {
            get { return !_isMatchInProgress; }
            set
            {
                SetPropertyAndNotify(ref _isMatchInProgress, value, () => IsMatchInProgress);
                if (_isMatchInProgress && _score != null)
                {

                }
            }
        }

        /// <summary>
        /// Message to display on screen
        /// </summary>
        private string _message = string.Empty;
        public string Message
        {
            get { return _message; }
            set { SetPropertyAndNotify(ref _message, value, () => Message); }
        }

        /// <summary>
        /// Dispose an object
        /// </summary>
        public void Dispose()
        {
            _disposable.Dispose();
        }

        /// <summary>
        /// Start match command
        /// </summary>
        private ICommand startMatchCommand;
        public ICommand StartMatchCommand
        {
            get
            {
                if (startMatchCommand == null) startMatchCommand = 
                        new RelayCommand(new Action<object>(OnStartMatch));
                return startMatchCommand;
            }
            set { SetProperty(ref startMatchCommand, value); }
        }

        /// <summary>
        /// On start match
        /// </summary>
        /// <param name="obj">Source object</param>
        private void OnStartMatch(object obj)
        {
            // Initialize empty for the SelectedSet
            SelectedSet = new KeyValuePair<Guid, string>();

            // Start the match
            MatachHandler.Start(Match, FirstPlayer, SecondPlayer, FirstPlayer);         
        }
    }
}