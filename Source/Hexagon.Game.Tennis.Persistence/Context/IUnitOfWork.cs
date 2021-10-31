using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Context
{
    /// <summary>
    /// Unit of work operations
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }

        /// <summary>
        /// Returns an instance of a type repository
        /// </summary>
        /// <typeparam name="T">Type of an entity</typeparam>
        /// <returns>Return an instance of type repository</returns>
        IRepository<T> Repository<T>() where T : class;
    }
}
