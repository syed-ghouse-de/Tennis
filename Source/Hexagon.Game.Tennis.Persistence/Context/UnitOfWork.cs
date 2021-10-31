using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Game.Tennis.Persistence.Context
{
    /// <summary>
    /// Unit of work operations
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private static IUnitOfWork _instance;

        public DbContext DbContext { get; private set; }
        private Dictionary<string, object> Repositories { get; }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="context">Persistence context</param>
        private UnitOfWork(PersistenceContext context)
        {
            DbContext = context;
            Repositories = new Dictionary<string, dynamic>();
        }

        /// <summary>
        /// Singleton instance of an object
        /// </summary>
        public static IUnitOfWork Instance
        {
            get
            {
                // Return an instance if already present             
                if (_instance == null)
                    _instance = new UnitOfWork(new PersistenceContext());

                // Return a instance
                return _instance;
            }
        }

        /// <summary>
        /// Returns an instance of a type repository
        /// </summary>
        /// <typeparam name="T">Type of an entity</typeparam>
        /// <returns>Return an instance of type repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            var typeName = type.Name;

            lock (Repositories)
            {
                // Return an instance of repository if already present in cache
                if (Repositories.ContainsKey(typeName))
                    return (IRepository<T>)Repositories[typeName];

                // Create an instance of a repository
                var repository = new Repository<T>(DbContext);
                Repositories.Add(typeName, repository);

                return repository;
            }
        }

        /// <summary>
        /// Dispose the database connection
        /// </summary>
        public void Dispose()
        {
            if (DbContext == null)
                return;
       
            // Close the connection
            if (DbContext.Database.GetDbConnection().State == ConnectionState.Open)    
                DbContext.Database.GetDbConnection().Close();
     
            DbContext.Dispose();
            DbContext = null;
        } 
    }
}
