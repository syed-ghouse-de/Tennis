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
        IPlayer Opponent { get; }                           // To get the opponent player
        IPoint Point { get; }                               // To maintain player current point    
        PlayerEntity Identity { get; }                      // To maintain the identity of player  

        /// <summary>
        /// Method for player win point
        /// </summary>       
        void Win();

        /// <summary>
        /// Method for player loose point
        /// </summary>
        void Loose();
    }
}
