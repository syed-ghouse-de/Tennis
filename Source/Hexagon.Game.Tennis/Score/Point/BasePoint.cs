using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Abstract base point class
    /// </summary>
    public abstract class BasePoint
    {
        public event Action<PlayerEntity, PlayerPoint> PointWin;
        public event Action<PlayerEntity> GamePointWin;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="point">Point</param>
        public BasePoint(PlayerPoint point)
        {
            Point = point;            
        }

        /// <summary>
        /// To maintain the point
        /// </summary>
        public PlayerPoint Point { get; set; }

        /// <summary>
        /// Property to get the handler of point action
        /// </summary>
        protected Action<PlayerEntity, PlayerPoint> PointWinHandler { get { return PointWin; } }

        /// <summary>
        /// Property to get the handler of GamePoint action
        /// </summary>
        protected Action<PlayerEntity> GamePointWinHandler { get { return GamePointWin; } }

        /// <summary>
        /// Execute point and game win handlers
        /// </summary>
        /// <param name="opponent">Opponent player</param>
        /// <param name="point">Winner point</param>
        protected void ExecuteHandlers(Player opponent, PlayerPoint point)
        {
        //    // Run the handlers asynchronously
        //    Task.Run(() =>
        //    {
        //        // Wait for half second before updating game and point data
        //        Thread.Sleep(500);

        //    // Invoke point & game point action handler
        //    PointWinHandler?.Invoke(opponent.Opponent.Identity, point);
        //    if (this is Advantage || this is Forty)
        //        GamePointWinHandler?.Invoke(opponent.Opponent.Identity);
        //});
        }
    }
}
