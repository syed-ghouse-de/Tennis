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
        public System.DateTime StartedOn { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public int Status { get; set; }

        public virtual StatusEntity StatusNavigation { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchEntity() { }
    }
}
