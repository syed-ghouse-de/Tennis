using Hexagon.Game.Tennis.Desktop.Command;
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
    /// Score view model
    /// </summary>
    public class ScoreViewModel : BaseViewModel, IDisposable
    {
        private IMatchHandler _matchHandler;
        private readonly CompositeDisposable _disposable;

        private ScoreModel _score;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScoreViewModel()
        {
            _matchHandler = MatchHandler.Instance;
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
        /// Match Handler
        /// </summary>
        public IMatchHandler MatachHandler { get { return _matchHandler; } }

        /// <summary>
        /// Match updated score
        /// </summary>
        public ScoreModel Score
        {
            get { return _score; }
            private set { SetPropertyAndNotify(ref _score, value, () => Score); }
        }

        /// <summary>
        /// Dispose an object
        /// </summary>
        public void Dispose()
        {
            _disposable.Dispose();
        }

        private ICommand clickCommand;
        public ICommand Click_Command
        {
            get
            {
                if (clickCommand == null) clickCommand = new RelayCommand(new Action<object>(Execute));
                return clickCommand;
            }
            set { SetProperty(ref clickCommand, value); }
        }

        private void Execute(object obj)
        {
            var handler = MatchHandler.Instance;
            handler.Start();
        }
    }
}
