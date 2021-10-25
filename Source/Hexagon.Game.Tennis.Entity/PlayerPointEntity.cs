using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Class to manage server & receiver players points
    /// </summary>
    public class PlayerPointEntity : BaseEntity
    {         
        public PlayerEntity Player { get; set; }
        public PlayerPoint Point { get; set; }        
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerPointEntity() { }
    }
}
