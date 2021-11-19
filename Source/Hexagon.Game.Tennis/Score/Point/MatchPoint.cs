using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Match point state
    /// </summary>
    public class MatchPoint : BasePoint, IPoint
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchPoint() : base(PlayerPoint.MatchPoint) { }

        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        public IPoint Loose()
        {
            throw new AlreadyMatchWonException();
        }

        /// <summary>
        /// Execute to maintain the state of player win point
        /// </summary>
        public IPoint Win(IPlayer opponent)
        {
            throw new AlreadyMatchWonException();
        }

        /// <summary>
        /// Update points of the player
        /// </summary>
        /// <param name="player">Player to which point to be updated</param>
        public void Update(IPlayer player)
        {
            throw new AlreadyMatchWonException();
        }
    }
}