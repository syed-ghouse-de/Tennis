using Hexagon.Game.Tennis.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Service
{
    /// <summary>
    /// Abastract class for persistence service
    /// </summary>
    public abstract class BasePersistenceService
    {
        /// <summary>
        /// Returns an instance of a type repository
        /// </summary>
        /// <typeparam name="T">Type of an entity</typeparam>
        /// <returns>Return an instance of type repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            // Return an instance of a given repository
            return UnitOfWork.Instance.Repository<T>();
        }
    }
}
