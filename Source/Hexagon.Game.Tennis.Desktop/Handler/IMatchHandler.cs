using Hexagon.Game.Tennis.Desktop.Model;
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
        /// 
        /// </summary>
        IMatch Match { get; }

        /// <summary>
        /// Score observable
        /// </summary>
        IObservable<ScoreModel> Score { get; }

        /// <summary>
        /// Start a match
        /// </summary>
        /// <param name="match">Match to start</param>
        /// <param name="firstPlayer">First player of the match</param>
        /// <param name="secondPlayer">Second player of the match</param>
        /// <param name="tossWon">Intial server of the Match</param>
        void Start(MatchModel match, PlayerEntity firstPlayer, 
            PlayerEntity secondPlayer, PlayerEntity tossWon);  
    }
}
