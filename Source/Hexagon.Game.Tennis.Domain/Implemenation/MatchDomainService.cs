using Hexagon.Game.Framework.Exceptions;
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
        // Member variable for player persistence service
        private IMatchPersistenceService _matchPersistenceService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchDomainService(IMatchPersistenceService matchPersistenceService)
        {
            _matchPersistenceService = matchPersistenceService;
        }

        /// <summary>
        /// Service method to add match
        /// </summary>
        /// <param name="match">Match details to add</param>
        public void AddMatch(MatchEntity match)
        {
            try
            {
                // Initialize match status and started data                
                match.Status = Status.NoStarted;
                _matchPersistenceService.AddMatch(match);
            }
            catch (PersistenceServiceException persistenceException)
            {
                throw new DomainServiceException(persistenceException.Message);
            }
            catch (Exception exception)
            {
                throw new DomainServiceException(exception.Message);
            }
        }


        /// <summary>
        /// Service method to get the match details
        /// </summary>
        /// <param name="id">Id of the match</param>
        /// <returns>Return MatchEntity type</returns>
        public MatchEntity GetMatch(Guid id)
        {
            try
            {
                // Get the match details by passing match id  
                return _matchPersistenceService.GetMatch(id);
            }
            catch (PersistenceServiceException persistenceException)
            {
                throw new DomainServiceException(persistenceException.Message);
            }
            catch (Exception exception)
            {
                throw new DomainServiceException(exception.Message);
            }
        }
    }
}
