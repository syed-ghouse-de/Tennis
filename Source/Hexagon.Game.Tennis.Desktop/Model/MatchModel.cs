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
        public DateTime Date { get; set; }
        public string Court { get; set; }   
        public PlayerModel WonBy { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchModel()
        {            
        }
    }
}
