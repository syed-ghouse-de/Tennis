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
        public DateTime StartedOn { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public Status Status { get; set; }
        public int BestOfSets { get; set; }
        public PlayerEntity WonBy { get; set; }
        public List<PlayerEntity> Players { get; set; }
        public List<SetEntity> Sets { get; set; }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchEntity() { }
    }
}
