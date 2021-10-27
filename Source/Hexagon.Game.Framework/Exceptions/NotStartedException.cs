using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Exception to throw when matach is not started
    /// </summary>
    public class NotStartedException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public NotStartedException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public NotStartedException(string message) : base(message) { }
    }
}
