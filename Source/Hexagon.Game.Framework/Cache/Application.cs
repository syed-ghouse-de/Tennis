using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Cache
{
    /// <summary>
    /// Managing application level cache
    /// </summary>
    public class Application
    {
        private readonly string PERSISTENCE_CONNECTION = "PersistenceConnection";

        // member variable to have single instance
        private static Application _instance;

        /// <summary>
        /// Private constructor
        /// </summary>
        private Application()
        {
            _persistenceConnection = ConfigurationManager
                .AppSettings.Get(PERSISTENCE_CONNECTION);
        }

        /// <summary>
        /// Get the instance of an application cache
        /// </summary>
        public static Application Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Application();

                return _instance;
            }
        }

        /// <summary>
        /// Property to get the database connection string
        /// </summary>
        private string _persistenceConnection = String.Empty;
        public string PersistenceConnection
        {
            get { return _persistenceConnection; }
        }
    }
}
