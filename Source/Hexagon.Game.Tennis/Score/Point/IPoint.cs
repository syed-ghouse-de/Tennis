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
        /// Property to maintain the point
        /// </summary>
        PlayerPoint Point { get; set; }

        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        IPoint Loose();

        /// <summary>
        /// Execute to maintain the state of player win point
        /// </summary>
        IPoint Win();
    }
}
