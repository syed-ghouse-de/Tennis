using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Domain.Service
{
    /// <summary>
    /// Interface to manage Score service 
    /// </summary>
    public interface IScoreDomainService : IDomainService
    {
        /// <summary>
        /// Service method to calculate and update player game point win 
        /// </summary>
        /// <param name="match">Match information</param>
        /// <param name="score">Match score</param>
        /// <param name="receiver">Receiver player</param>
        /// <param name="winPlayer">Winning player</param>
        /// <returns>Returns score details</returns>
        ScoreEntity GamePointWin(MatchEntity match, 
            ScoreEntity score, PlayerEntity receiver, PlayerEntity winPlayer);

        /// <summary>
        /// Service method to calculate and update player point win
        /// </summary>
        /// <param name="score">Match score</param>
        /// <param name="server">Server of the game</param>
        /// <param name="winner">Point winner</param>
        /// <param name="looser">Point looser</param>
        /// <param name="point">Winning point</param>
        /// <returns>Returns score details</returns>
        ScoreEntity PointWin(ScoreEntity score, PlayerEntity server,
            PlayerEntity winner, PlayerEntity looser, PlayerPoint point);

        /// <summary>
        /// Get the score of current match
        /// </summary>
        /// <param name="match">Match information</param>
        /// <param name="server">Name of the server player</param>
        /// <returns>Returns score details</returns>
        ScoreEntity GetMatchScore(MatchEntity match, PlayerEntity server);
    }
}
