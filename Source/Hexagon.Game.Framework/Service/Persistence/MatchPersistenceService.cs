using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Framework.Service.Persistence
{
    /// <summary>
    /// Wrapper persistence class for Match
    /// </summary>
    public class MatchPersistenceService : IPersistenceService
    {       
        /// <summary>
        /// Method to get an instance of MatchPersistenceService of the targeted database
        /// </summary>
        /// <returns>Returns an instance of a match persistence object</returns>
        public IPersistenceService Instance()
        {
            // Create an instance of a match persistence service 
            // for a targeted database
            string assemblyName = "Hexagon.Game.Tennis.Persistence";            
            var instance = (IMatchPersistenceService)Activator.CreateInstance(
                assemblyName, string.Format("{0}.{1}", assemblyName, this.GetType().Name)).Unwrap();

            return instance;
        }
    }
}
