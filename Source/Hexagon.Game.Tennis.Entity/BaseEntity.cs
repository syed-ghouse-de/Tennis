using Hexagon.Game.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Base abstract class for entity
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Id to manage unique identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
