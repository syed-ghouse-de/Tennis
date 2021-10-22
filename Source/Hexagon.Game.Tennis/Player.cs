using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Framework.Entity;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Player class to export player functionalities
    /// </summary>
    public class Player : PlayerEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Player() : base() { }

        /// <summary>
        /// Player parametarized constructror
        /// </summary>
        /// <param name="entity">Player entity</param>
        public Player(PlayerEntity entity) : base(entity) { }
    }

    public class Players : EntityList<PlayerEntity>
    {
        public Players() { }

        /// <summary>
        /// To get the first player of the match
        /// </summary>
        public Player FirstPlayer { get; set; }

        /// <summary>
        /// To get the Second player of the match
        /// </summary>
        public Player SecondPlayer { get; set; }
    }
}
