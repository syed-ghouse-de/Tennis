using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Player template 
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// PointWin action event for notifying player point win
        /// </summary>
        event Action<PlayerEntity, PlayerPoint> PointWin;   // Delegate for player point win

        /// <summary>
        /// GamePointWin action event for notifying player game point win
        /// </summary>
        event Action<PlayerEntity> GamePointWin;            // Delegat for player game point win

        /// <summary>
        /// Deuce action event for notifying Deuce 
        /// </summary>
        event Action<PlayerEntity> Deuce;                   // Delegate for Deuce point

        /// <summary>
        /// Opponent player
        /// </summary>
        IPlayer Opponent { get; }                           // To get the opponent player

        /// <summary>
        /// Player point
        /// </summary>
        IPoint Point { get; }                               // To maintain player current point   
        
        /// <summary>
        /// Player identification
        /// </summary>
        PlayerEntity Identity { get; }                      // To maintain the identity of player  

        /// <summary>
        /// Method for player win point
        /// </summary>       
        void Win();

        /// <summary>
        /// Method for player loose point
        /// </summary>
        void Loose();

        /// <summary>
        /// Method to set the Love point for the player
        /// </summary>
        void SetLove();

        /// <summary>
        /// Method to set the Deuce for the player
        /// </summary>
        void SetDeuce();

        /// <summary>
        /// Method to set the Match point for the player
        /// </summary>
        void SetMatchPoint();
    }
}
