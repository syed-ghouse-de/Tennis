using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Advantage point state
    /// </summary>
    public class Advantage : BasePoint, IPoint
    { 
        /// <summary>
        /// Default constructor
        /// </summary>
        public Advantage() : base(PlayerPoint.Advantage) { }

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
        public IPoint Win(IPlayer opponent)
        {
            // Return Gamepoint point for the player    
            return new GamePoint();
        }

        /// <summary>
        /// Update points of the player
        /// </summary>
        /// <param name="player">Player to which point to be updated</param>
        public void Update(IPlayer player)
        {
            // Invoke point win handler to update the player point
            PointWinHandler?.Invoke(player.Identity, PlayerPoint.Advantage);            
        }
    }
}
