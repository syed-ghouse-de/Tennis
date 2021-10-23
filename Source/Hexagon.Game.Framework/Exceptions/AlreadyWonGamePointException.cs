using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Exception class for game point won
    /// </summary>
    public class AlreadyWonGamePointException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public AlreadyWonGamePointException()
        { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public AlreadyWonGamePointException(string message) : base(message) { }
    }
}
