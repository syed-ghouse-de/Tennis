using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Domain
{
    /// <summary>
    /// Interface to manage match service
    /// </summary>
    public interface IMatchDomainService
    {
        /// <summary>
        /// Service method to add match
        /// </summary>
        /// <param name="match">Match details to add</param>
        MatchEntity AddMatch(MatchEntity match);

        /// <summary>
        /// Service method to start the match
        /// </summary>
        /// <param name="match">Match details to update</param>
        /// <returns>Returns updated match details</returns>
        MatchEntity StartMatch(MatchEntity match);

        /// <summary>
        /// Service method to get the match details
        /// </summary>
        /// <param name="id">Id of the match</param>
        /// <returns>Return MatchEntity type</returns>
        MatchEntity GetMatch(Guid id);
    }
}
