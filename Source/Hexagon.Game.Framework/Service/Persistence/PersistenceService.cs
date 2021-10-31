using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Factory service to create an instance for the provided type 
    /// </summary>
    public class PersistenceService 
    {
        /// <summary>
        /// Method to create an instance for the provided type
        /// </summary>
        /// <typeparam name="T">Type of a service to create an instance</typeparam>
        /// <returns>Returns an insance of a provided type</returns>
        public static T Instance<T>() where T : IPersistenceService,  new()
        {
            // Create an instance of a provided type
            T instance = new T();
            return instance;
        }
    }
}
