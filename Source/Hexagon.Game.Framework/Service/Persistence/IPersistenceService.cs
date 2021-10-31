using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Interface for persistence service
    /// </summary>
    public interface IPersistenceService : IService
    {
        /// <summary>
        /// Method to get an instance of a persistence service
        /// </summary>
        /// <returns></returns>
        IPersistenceService Instance();
    }
}
