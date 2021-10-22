using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Status of the Mmatch, Set & Game
    /// </summary>
    public enum Status
    {
        NoStarted,
        InProgress,
        Completed
    }

    /// <summary>
    /// Differnet point of the game
    /// </summary>
    public enum PlayerPoint
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3,
        Advantage = 4,
        Deuce = 5,
        GamePoint = 6
    }
}
