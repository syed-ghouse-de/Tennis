using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Match data transfer object to store match details
    /// </summary>
    public class MatchEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Court { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchEntity() { }
    }
}
