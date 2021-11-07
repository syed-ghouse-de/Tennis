using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Handler
{
    /// <summary>
    /// Match handler interface for observing Score
    /// </summary>
    public interface IMatchHandler
    {
        /// <summary>
        /// Score observable
        /// </summary>
        IObservable<ScoreEntity> Score { get; }

        /// <summary>
        /// Start a match
        /// </summary>
        void Start();
    }
}
