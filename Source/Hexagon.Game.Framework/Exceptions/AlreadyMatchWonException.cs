using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Exception class for match won
    /// </summary>
    public class AlreadyMatchWonException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public AlreadyMatchWonException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public AlreadyMatchWonException(string message) : base(message) { }
    }
}


