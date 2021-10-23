﻿using System;
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
        public IPoint Win(Player opponent)
        {
            throw new AlreadyWonGamePointException();
        }
    }
}
