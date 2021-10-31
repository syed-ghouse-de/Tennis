using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Context
{
    /// <summary>
    /// Repository for entity operations
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entities { get; }
        DbContext DbContext { get; }

        /// <summary>
        /// Insert record into database 
        /// </summary>
        /// <param name="entity">Entity to insert in database</param>
        void Add(T entity);

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="entity">Entity to update in database</param>
        void Update(T entity);

        /// <summary>
        /// Remove record from database 
        /// </summary>
        /// <param name="entity">Entity to remove from database</param>
        void Remove(T entity);

        /// <summary>
        /// Find a record in database
        /// </summary>
        /// <param name="keys">Unique key to search in the database</param>
        /// <returns>Returns a record</returns>
        T Find(params object[] keys);

        /// <summary>
        /// Get all the records for the database
        /// </summary>
        /// <returns>Retursn list of records</returns>
        IList<T> GetAll();
    }
}
