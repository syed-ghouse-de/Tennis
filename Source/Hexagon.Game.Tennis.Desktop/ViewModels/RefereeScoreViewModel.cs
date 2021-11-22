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

        private ScoreModel _score;

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
        public ScoreModel Score
        {
            get { return _score; }
            set
            {
                // Set and notifiy the change of Score data
                SetPropertyAndNotify(ref _score, value, () => Score);
                IsMatchInProgress = Score.Match.Status.Equals(Status.InProgress);               // Check if match is in progress

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

                // Add default values to both the pla
                GamesWon = new List<int>() { Score.Players[0].GamesWon[SelectedSet.Key], Score.Players[1].GamesWon[SelectedSet.Key] };         
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
            set { SetPropertyAndNotify(ref _isMatchInProgress, value, () => IsMatchInProgress); }
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
                if (startMatchCommand == null) startMatchCommand = new RelayCommand(new Action<object>(OnStartMatch));
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
            try
            {       
                // Initialize match data
                MatchEntity match = new MatchEntity();

                match.BestOfSets = Match.BestOfSets;
                match.Name = Match.Name;
                match.Players.Add(new PlayerEntity() { FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) });
                match.Players.Add(new PlayerEntity() { FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) });

                // Initialize new match data by calling initialize method
                MatachHandler.Initialize(match);
                MatachHandler.Match.Players.Server = MatachHandler.Match.Players.FirstPlayer;

                // Start the match
                MatachHandler.Start();
            }
            catch (Exception exception)
            {

            }
        }
    }
}