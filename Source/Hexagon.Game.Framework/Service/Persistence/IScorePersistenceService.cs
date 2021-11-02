using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Interface for score persistence operations
    /// </summary>
    public interface IScorePersistenceService : IPersistenceService
    {
        /// <summary>
        /// Add Set into database
        /// </summary>
        /// <param name="matchId">Add a Set for specified Match</param>
        /// <param name="set">Add as Set into database</param>
        void AddSet(Guid matchId, SetEntity set);

        /// <summary>
        /// Update Set
        /// </summary>
        /// <param name="set">Set to update in the database</param>
        void UpdateSet(SetEntity set);

        /// <summary>
        /// Add a game in to database
        /// </summary>
        /// <param name="setId">Add a game for a specified Set</param>
        /// <param name="game">Game to add into database</param>
        void AddGame(Guid setId, GameEntity game);

        /// <summary>
        /// Update a Game into database
        /// </summary>
        /// <param name="game">Game to update in database</param>
        void UpdateGame(GameEntity game);

        /// <summary>
        /// Add a point into database
        /// </summary>
        /// <param name="gameId">Add a point for a specified game</param>
        /// <param name="point">Add a point into database</param>
        void AddPoint(Guid gameId, PlayerPointEntity point);        
    }
}
