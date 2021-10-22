using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Entity
{
    /// <summary>
    /// Entity interface for data transfer objects
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique entity identifer
        /// </summary>
        Guid Id { get; set; }
    }
}
