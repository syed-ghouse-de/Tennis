using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Class to manage game
    /// </summary>
    public class GameEntity : BaseEntity
    {
        public PlayerEntity Server { get; set; }
        public PlayerEntity WonBy { get; set; }
        public Status Status { get; set; }
        public List<PlayerPointEntity> PlayerPoints { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameEntity() { }

        /// <summary>
        /// Get the Point details for the specicific set number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public PlayerPointEntity GetPlayerPoint(int number)
        {
            return PlayerPoints[number];
        }
    }
}
