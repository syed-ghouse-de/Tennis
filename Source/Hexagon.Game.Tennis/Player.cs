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

        public event Action<PlayerEntity, PlayerPoint> PointWin;            // Delegate for poin win
        public event Action<PlayerEntity> GamePointWin;                     // Delegate for game point win
        public event Action<PlayerEntity> Deuce;                            // Delegate for Deuce point 

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
        /// Method for player win point
        /// </summary>        
        public void Win()
        {
            _point = Point.Win((Player)Opponent);

            // Subscribe event handlers
            _point.PointWin += OnPointWin;
            _point.GamePointWin += OnGamePointWin;

            // Invoke Deuce action handler if the current point is Deuce             
            if (_point is Deuce)
                Deuce?.Invoke(Identity);      
        }

        /// <summary>
        /// Method for player loose point
        /// </summary>
        public void Loose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inititialize default values
        /// </summary>
        private void Init()
        {
            // Initialize default to Love point
            _point = new Love();
            _point.PointWin += OnPointWin;
        }

        /// <summary>
        /// Method to set the Deuce for the player
        /// </summary>
        public void SetDeuce()
        {
            _point = new Deuce();

            _point.PointWin += OnPointWin;
            _point.GamePointWin += OnGamePointWin;
        }

        /// <summary>
        /// Event action for player point win
        /// </summary>
        /// <param name="winPlayer">Winner player</param>
        /// <param name="point">Winner point</param>
        private void OnPointWin(PlayerEntity winPlayer, PlayerPoint point)
        {
            // Invoke event action
            PointWin?.Invoke(winPlayer, point);    
        }

        /// <summary>
        /// Event action for player Game Point
        /// </summary>
        /// <param name="player">Information of the game point player</param>
        private void OnGamePointWin(PlayerEntity player)
        {
            // Invoke event action
            GamePointWin?.Invoke(player);
        }
    }

    /// <summary>
    /// Class to maintain the list of Players
    /// </summary>
    public class Players : EntityList<Player>
    {
        private readonly byte FIRST_PLAYER = 0;                              // Constant to maintain the index of first player
        private readonly byte SECOND_PLAYER = 1;                             // Constant to maintain the index of second player        

        public event Action<PlayerEntity, PlayerPoint> PointWin;            // Delegate for point win
        public event Action<PlayerEntity> GamePointWin;                     // Delegate for game point win         

        /// <summary>
        /// Default constructor
        /// </summary>
        public Players() { }

        /// <summary>
        /// Indexer to return IPlayer object
        /// </summary>
        /// <param name="id">Guid of a Player</param>
        /// <returns></returns>
        public IPlayer this[Guid id]
        {
            get
            {
                return this.Where(p => p.Id.Equals(id)).FirstOrDefault();
            }            
        }

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

        /// <summary>
        /// Set the current server
        /// </summary>
        public IPlayer Server { get; set; }


        /// <summary>
        /// Add a player in to the list
        /// </summary>
        /// <param name="player">Player to add</param>
        public void Add(IPlayer player)
        {            
            // Check the number of players added in to the list, throw an exception if more than 2 players is added
            if (this.Count >= 2)
                throw new InvalidOperationException("Can not add more than 2 players!");

            // Check the duplication of the player, throw an exception when duplicate found
            var players = this.Where(pl => pl.Unique(player.Identity)).Select(entity => entity.Identity);
            if (players.Any())
                throw new DuplicateException(string.Format("Player {0}, {1} already exists!", player.Identity.SurName, player.Identity.FirstName));

            player.PointWin += OnPointWin;
            player.GamePointWin += OnGamePointWin;
            player.Deuce += OnDeuce;

            base.Add((Player) player);
        } 

        /// <summary>
        /// Player point win
        /// </summary>
        /// <param name="winPlayer">Winner player</param>
        /// <param name="point">Player winner point</param>
        private void OnPointWin(PlayerEntity winPlayer, PlayerPoint point)
        {
            PointWin?.Invoke(winPlayer, point);
        }

        /// <summary>
        /// Player game point win
        /// </summary>
        /// <param name="player">Game point winner</param>
        private void OnGamePointWin(PlayerEntity player)
        {
            GamePointWin?.Invoke(player);
        }

        private void OnDeuce(PlayerEntity winner)
        {
            // Set the Deuce point for both the players
            FirstPlayer.SetDeuce();
            SecondPlayer.SetDeuce();
        }
    }
}
