using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Framework.Entity;
using Hexagon.Game.Framework.Exceptions;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Player class to export player functionalities
    /// </summary>
    public class Player : PlayerEntity
    {
        public PlayerEntity Identity { get { return (PlayerEntity)this; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player() : base() { }

        /// <summary>
        /// Player parametarized constructror
        /// </summary>
        /// <param name="entity">Player entity</param>
        public Player(PlayerEntity entity) : base(entity) { }

        public Player(Player player) : base(player.Identity) { }     
    }

    public class Players : EntityList<Player>
    {
        public Players() { }

        /// <summary>
        /// To get the first player of the match
        /// </summary>
        public Player FirstPlayer
        {
            get { return new Player(this[0]); }
        }

        /// <summary>
        /// To get the Second player of the match
        /// </summary>
        public Player SecondPlayer
        {
            get { return new Player(this[1]); }
        }

        new public void Add(Player player)
        {
            // Check the number of players added in to the list, throw an exception if more than 2 players is added
            if (this.Count >= 2)
                throw new InvalidOperationException("Can not add more than 2 players!");

            // Check the duplication of the player, throw an exception when duplicate found
            var players = this.Where(pl => pl.Unique(player)).Select(entity => entity.Identity);
            if (players.Any())
                throw new DuplicateException(string.Format("Player {0}, {1} already exists!", player.SurName, player.FirstName));            

            base.Add(player);
        }
    }
}
