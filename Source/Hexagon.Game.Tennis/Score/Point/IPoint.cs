using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Interface to maintain the state of player point
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// To maintain the point state
        /// </summary>
        PlayerPoint Point { get; set; }

        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        IPoint Loose();

        /// <summary>
        /// Execute to maintain the state of player win point         
        /// </summary>
        /// <param name="opponent">Opponent player</param>
        /// <returns>Returns latest state of the point</returns>
        IPoint Win(Player opponent);
    }
}
