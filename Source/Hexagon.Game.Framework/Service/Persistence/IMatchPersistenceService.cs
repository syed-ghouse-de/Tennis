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
        MatchEntity GetMatch(Guid id);
    }
}
