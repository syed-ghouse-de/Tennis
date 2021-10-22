using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Exceptions
{
    /// <summary>
    /// Exception class for duplicate
    /// </summary>
    public class DuplicateException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public DuplicateException()
        { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public DuplicateException(string message) : base(message) { }
    }
}
