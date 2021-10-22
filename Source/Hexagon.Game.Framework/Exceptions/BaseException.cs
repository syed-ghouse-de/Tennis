using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Framework abstract base exception class
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public BaseException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public BaseException(string message) : base(message) { }
    }
}
