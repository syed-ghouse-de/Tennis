using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Service
{
    /// <summary>
    /// Class for player persistence service
    /// </summary>
    public class PlayerPersistenceService : BasePersistenceService, IPlayerPersistenceService
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerPersistenceService() { }

        /// <summary>
        /// Object Instance
        /// </summary>
        /// <returns>Returns an insance of Player PersistenceService</returns>
        public IPersistenceService Instance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the players
        /// </summary>
        /// <returns>List of player of type PlayerEntity</returns>
        public List<PlayerEntity> GetPlayers()
        {
            try
            {
                // Get all the players from database
                var players = Repository<PlayerModel>().GetAll()
                    // Prepare player entity
                    .Select(p => new PlayerEntity()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        SurName = p.SurName,
                        LastName = p.LastName,
                        DateOfBirth = p.DateOfBirth,
                        Club = p.Club
                    }).ToList();

                // Return list of playes
                return players;
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }
    }
}
