using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Class to manage player points
    /// </summary>
    public class PointEntity : BaseEntity
    {      
        /// <summary>
        /// Property to store Point
        /// </summary>
        public PlayerPoint Point { get; set; }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PointEntity() { }  
    }
}
