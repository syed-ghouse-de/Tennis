using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Love state point
    /// </summary>
    public class Love : BasePoint, IPoint
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Love() : base(PlayerPoint.Love) { }

        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        public IPoint Loose()
        {
            // If player looses, it say's in the same point
            return this;
        }

        /// <summary>
        /// Execute to maintain the state of player win point
        /// </summary>
        /// <param name="opponent">Opponent player</param>
        /// <returns>Returns latest state of the point</returns>
        public IPoint Win(Player opponent)
        {
            // Return Fifteen point if palyer wins.
            return new Fifteen();
        }
    }
}
