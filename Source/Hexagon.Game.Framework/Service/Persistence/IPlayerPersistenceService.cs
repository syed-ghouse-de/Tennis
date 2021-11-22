using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Interface for player persistence service
    /// </summary>
    public interface IPlayerPersistenceService : IPersistenceService
    {
        /// <summary>
        /// Get all the players
        /// </summary>
        /// <returns>List of player of type PlayerEntity</returns>
        List<PlayerEntity> GetPlayers();
    }
}
