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
    /// Interface to manage player service
    /// </summary>
    public class PlayerDomainService : IPlayerDomainService
    {
        // Member variable for player persistence service
        private IPlayerPersistenceService _playerPersistenceService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerDomainService(IPlayerPersistenceService playerPersistenceService)
        {
            _playerPersistenceService = playerPersistenceService;
        }

        /// <summary>
        /// Get all the players
        /// </summary>
        /// <returns>Return list of players</returns>
        public List<PlayerEntity> GetPlayers()
        {
            try
            {
                // Get all the players from database
                return _playerPersistenceService.GetPlayers();                    
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
