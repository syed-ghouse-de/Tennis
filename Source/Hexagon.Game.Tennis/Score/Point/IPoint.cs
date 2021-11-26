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
        /// PointWin action event for notifying player point win
        /// </summary>
        event Action<PlayerEntity, PlayerPoint> PointWin;

        /// <summary>
        /// GamePointWin action event for notifying player game point win
        /// </summary>
        event Action<PlayerEntity> GamePointWin;

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
        IPoint Win(IPlayer opponent);

        /// <summary>
        /// Update points of the player
        /// </summary>
        /// <param name="player">Player to which point to be updated</param>
        void Update(IPlayer player);
    }
}
