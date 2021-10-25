using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Forty point state
    /// </summary>
    public class Forty : BasePoint, IPoint
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Forty() : base(PlayerPoint.Forty) { }

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
            // Invoke point & game point action handler
            PointWinHandler?.Invoke(opponent.Opponent.Identity, PlayerPoint.GamePoint);            
            GamePointWinHandler?.Invoke(opponent.Opponent.Identity);

            // Game point for the player
            return new GamePoint();
        }
    }
}

