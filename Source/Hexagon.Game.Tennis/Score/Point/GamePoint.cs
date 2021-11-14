using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Game point state
    /// </summary>
    public class GamePoint : BasePoint, IPoint
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GamePoint() : base(PlayerPoint.GamePoint) { }

        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        public IPoint Loose()
        {
            throw new AlreadyWonGamePointException();
        }

        /// <summary>
        /// Execute to maintain the state of player win point
        /// </summary>
        public IPoint Win(IPlayer opponent)
        {
            throw new AlreadyWonGamePointException();            
        }

        /// <summary>
        /// Update points of the player
        /// </summary>
        /// <param name="player">Player to which point to be updated</param>
        public void Update(IPlayer player)
        {
            // Invoke game nd point win hanlder to update the player point
            PointWinHandler?.Invoke(player.Identity, PlayerPoint.GamePoint);
            GamePointWinHandler?.Invoke(player.Identity);
        }
    }
}
