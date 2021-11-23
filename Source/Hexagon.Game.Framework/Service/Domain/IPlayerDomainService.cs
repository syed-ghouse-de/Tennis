using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Domain
{
    /// <summary>
    /// Interface to manage player service
    /// </summary>
    public interface IPlayerDomainService
    {
        /// <summary>
        /// Get all the players
        /// </summary>
        /// <returns>Return list of players</returns>
        List<PlayerEntity> GetPlayers();
    }
}
