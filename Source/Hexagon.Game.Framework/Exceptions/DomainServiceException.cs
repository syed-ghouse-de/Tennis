using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Domain service exception
    /// </summary>
    public class DomainServiceException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public DomainServiceException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public DomainServiceException(string message) : base(message) { }
    }
}
