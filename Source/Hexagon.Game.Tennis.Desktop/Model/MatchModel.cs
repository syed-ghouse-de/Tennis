using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Model
{
    /// <summary>
    /// Math model
    /// </summary>
    public class MatchModel
    {
        public string Name { get; set; }
        public DateTime StartedOn { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public Status Status { get; set; }
        public int BestOfSets { get; set; }
        public PlayerModel WonBy { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchModel()
        {
            CompletedOn = null;
            WonBy = null;
        }
    }
}
