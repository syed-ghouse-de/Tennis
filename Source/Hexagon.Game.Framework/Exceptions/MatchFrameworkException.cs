using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Match framework exception
    /// </summary>
    public class MatchFrameworkException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public MatchFrameworkException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public MatchFrameworkException(string message) : base(message) { }
    }
}
