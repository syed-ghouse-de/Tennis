using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Domain.Implemenation
{
    /// <summary>
    /// Interface to manage match service
    /// </summary>
    public class MatchDomainService : IMatchDomainService
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchDomainService(IMatchPersistenceService _matchPersistenceService)
        {
   
        }

        /// <summary>
        /// Service method to add match
        /// </summary>
        /// <param name="match">Match details to add</param>
        public void AddMatch(MatchEntity match)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Service method to get the match details
        /// </summary>
        /// <param name="id">Id of the match</param>
        /// <returns>Return MatchEntity type</returns>
        public MatchEntity GetMatch(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
