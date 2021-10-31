using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Interface for match persistence service
    /// </summary>
    public interface IMatchPersistenceService : IPersistenceService
    {
        /// <summary>
        /// Insert match details into database
        /// </summary>
        /// <param name="match">Match information to insert into database</param>
        void AddMatch(MatchEntity match);

        /// <summary>
        /// Get the match infomation
        /// </summary>
        /// <param name="id">Math id to get the match details</param>
        /// <returns></returns>
        IEnumerable<MatchEntity> GetMatch(Guid id);
    }
}
