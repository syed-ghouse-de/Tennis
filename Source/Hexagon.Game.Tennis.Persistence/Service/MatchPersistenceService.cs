using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Context;

namespace Hexagon.Game.Tennis.Persistence.Service
{
    /// <summary>
    /// Class for match persistence service
    /// </summary>
    public class MatchPersistenceService : IMatchPersistenceService
    {
        /// <summary>
        /// Insert match details into database
        /// </summary>
        /// <param name="match">Match information to insert into database</param>
        public void AddMatch(MatchEntity match)
        {           
            UnitOfWork.Instance.Repository<MatchEntity>().Add(match);
        }

        /// <summary>
        /// Get the match infomation
        /// </summary>
        /// <param name="id">Math id to get the match details</param>
        /// <returns></returns>
        public IEnumerable<MatchEntity> GetMatch(Guid id)
        {
            return UnitOfWork.Instance.Repository<MatchEntity>().Entities.Where(s => s.Status == 2).ToList();
        }

        public IPersistenceService Instance()
        {
            throw new NotImplementedException();
        }
    }
}
