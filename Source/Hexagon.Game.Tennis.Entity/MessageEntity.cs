using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Entity to manage error and warning messages
    /// </summary>
    public class MessageEntity : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageEntity()
        { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public MessageEntity(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Message of error or warning
        /// </summary>
        public string Message { get; set; }
    }
}
