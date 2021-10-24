using Hexagon.Game.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework
{
    /// <summary>
    /// Exception class for game point won
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Default exception constructor
        /// </summary>
        public NotFoundException() { }

        /// <summary>
        /// Constructor which passes error message
        /// </summary>
        /// <param name="message">Exception message</param>
        public NotFoundException(string message) : base(message) { }
    }
}
