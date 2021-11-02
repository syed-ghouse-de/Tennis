using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Persistence service exception
    /// </summary>
    public class PersistenceServiceException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public PersistenceServiceException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public PersistenceServiceException(string message) : base(message) { }
    }
}
