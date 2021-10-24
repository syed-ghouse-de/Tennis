using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Entity;
using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Score;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Player class to export player functionalities
    /// </summary>
    public class Player : PlayerEntity, IPlayer
    {
        internal IPoint _point;

        public IPlayer Opponent { get; set; }                               // To get the opponent player
        public PlayerEntity Identity { get { return (PlayerEntity)this; } } // To maintain the identity of player  
        public IPoint Point { get { return _point; } }                      // To maintain player current point

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player() : base()
        {
            Init();
        }

        /// <summary>
        /// Player parametarized constructror
        /// </summary>
        /// <param name="entity">Player entity</param>
        public Player(PlayerEntity entity) : base(entity)
        {
            Init();
        }

        /// <summary>
        /// Player parametarized constructor
        /// </summary>
        /// <param name="player"></param>
        public Player(IPlayer player) : base(player.Identity)
        {
            Init();
        }

        /// <summary>
        /// Inititializ default values
        /// </summary>
        private void Init()
        {
            // Initialize default to Love point
            _point = new Love();            
        }

        /// <summary>
        /// Method for player win point
        /// </summary>        
        public void Win()
        {
            _point = Point.Win((Player)Opponent);
        }

        /// <summary>
        /// Method for player loose point
        /// </summary>
        public void Loose()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class to maintain the list of Players
    /// </summary>
    public class Players : EntityList<Player>
    {
        private readonly byte FIRST_PLAYER = 0;                              // Constant to maintain the index of first player
        private readonly byte SECOND_PLAYER = 1;                              // Constant to maintain the index of second player

        /// <summary>
        /// Default constructor
        /// </summary>
        public Players() { }

        /// <summary>
        /// To get the first player of the match
        /// </summary>
        public IPlayer FirstPlayer
        {
            get
            {
                Player player = this[FIRST_PLAYER];
                player.Opponent = this[SECOND_PLAYER];

                return player;
            }
        }

        /// <summary>
        /// To get the Second player of the match
        /// </summary>
        public IPlayer SecondPlayer
        {
            get
            {
                Player player = this[SECOND_PLAYER];
                player.Opponent = this[FIRST_PLAYER];

                return player;
            }
        }

        public void Add(IPlayer player)
        {            
            // Check the number of players added in to the list, throw an exception if more than 2 players is added
            if (this.Count >= 2)
                throw new InvalidOperationException("Can not add more than 2 players!");

            // Check the duplication of the player, throw an exception when duplicate found
            var players = this.Where(pl => pl.Unique(player.Identity)).Select(entity => entity.Identity);
            if (players.Any())
                throw new DuplicateException(string.Format("Player {0}, {1} already exists!", player.Identity.SurName, player.Identity.FirstName));            

            base.Add((Player) player);
        }
    }
}
