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
    /// Spectator score view model
    /// </summary>
    public class SpectatorScoreViewModel : BaseViewModel, IDisposable
    {
        private IMatchHandler _matchHandler;
        private readonly CompositeDisposable _disposable;

        private ScoreModel _score;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpectatorScoreViewModel(IMatchHandler handler)
        {
            // Initialize match handler instance
            _matchHandler = handler;

            // Subscribe for Score change
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
        public IMatchHandler MatchHandler { get { return _matchHandler; } }

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
    }
}
