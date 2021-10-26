using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Thirty point state
    /// </summary>
    public class Thirty : BasePoint, IPoint
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Thirty() : base(PlayerPoint.Thirty) { }

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
            IPoint point = new Forty();

            // If both player are in Forty, return Deuce
            if (point.Point.Equals(opponent.Point.Point)) 
                point = new Deuce();  

            // Invoke point action handler
            PointWinHandler?.Invoke(opponent.Opponent.Identity, PlayerPoint.Forty);

            // Return point
            return point;
        }
    }
}
