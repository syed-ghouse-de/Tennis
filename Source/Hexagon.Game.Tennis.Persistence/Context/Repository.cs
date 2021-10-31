using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Game.Tennis.Persistence.Context
{
    /// <summary>
    /// Repository for entity operations
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Entities => DbContext.Set<T>();
        public DbContext DbContext { get; private set; }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Insert record into database 
        /// </summary>
        /// <param name="entity">Entity to insert in database</param>
        public void Add(T entity)
        {            
            Entities.Add(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="entity">Entity to update in database</param>
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove record from database 
        /// </summary>
        /// <param name="entity">Entity to remove from database</param>
        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find a record in database
        /// </summary>
        /// <param name="keys">Unique key to search in the database</param>
        /// <returns>Returns a record</returns>
        public T Find(params object[] keys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the records for the database
        /// </summary>
        /// <returns>Retursn list of records</returns>
        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
