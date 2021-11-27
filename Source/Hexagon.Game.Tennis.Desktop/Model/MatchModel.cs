using Hexagon.Game.Framework.MVVM.ViewModel;
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
    public class MatchModel : MatchEntity
    {
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
