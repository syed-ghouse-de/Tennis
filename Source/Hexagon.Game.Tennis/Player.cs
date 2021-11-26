using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IPoint _point;

        /// <summary>
        /// PointWin action event for notifying player point win
        /// </summary>
        public event Action<PlayerEntity, PlayerPoint> PointWin;            // Delegate for poin win

        /// <summary>
        /// GamePointWin action event for notifying player game point win
        /// </summary>
        public event Action<PlayerEntity> GamePointWin;                     // Delegate for game point win

        /// <summary>
        /// Deuce action event for notifying Deuce 
        /// </summary>
        public event Action<PlayerEntity> Deuce;                            // Delegate for Deuce point 

        /// <summary>
        /// Opponent player
        /// </summary>
        public IPlayer Opponent { get; set; }                               // To get the opponent player

        /// <summary>
        /// Player identification
        /// </summary>
        public PlayerEntity Identity { get { return (PlayerEntity)this; } } // To maintain the identity of player  

        /// <summary>
        /// Player point
        /// </summary>
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
            // Reset both he players to Love if any of the player has won the game
            if (_point.Point.Equals(PlayerPoint.GamePoint) ||
                Opponent.Point.Point.Equals(PlayerPoint.GamePoint))
            {
                SetLove();
                Opponent.SetLove();
            }

            _point = Point.Win((Player)Opponent);

            // Subscribe event handlers
            _point.PointWin += OnPointWin;
            _point.GamePointWin += OnGamePointWin;

            // Invoke Deuce action handler if the current point is Deuce             
            if (_point is Deuce)
                Deuce?.Invoke(Identity);

            // Trigger to update the point of the player
            _point.Update(this);
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
            SetLove();
        }

        /// <summary>
        /// Method to set the Love point for the player
        /// </summary>
        public void SetLove()
        {
            _point = new Love();

            _point.PointWin += OnPointWin;
            _point.GamePointWin += OnGamePointWin;
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
        /// Method to set the Match point for the player
        /// </summary>
        public void SetMatchPoint()
        {
            _point = new MatchPoint();
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
            _point = new GamePoint();
            // Invoke event action
            GamePointWin?.Invoke(player);            
        }
    }

    /// <summary>
    /// Class to maintain the list of Players
    /// </summary>
    public class Players  
    {
        private readonly byte FIRST_PLAYER = 0;                             // Constant to maintain the index of first player
        private readonly byte SECOND_PLAYER = 1;                            // Constant to maintain the index of second player        

        private EntityList<Player> _players = new EntityList<Player>();     // To maintain the players list         
        /// <summary>
        /// PointWin action event for notifying player point win
        /// </summary>
        public event Action<PlayerEntity, PlayerPoint> PointWin;            // Delegate for point win

        /// <summary>
        /// GamePointWin action event for notifying player game point win
        /// </summary>
        public event Action<PlayerEntity> GamePointWin;                     // Delegate for game point win         

        /// <summary>
        /// Default constructor
        /// </summary>
        public Players()
        {
            // Remove the players if any
            this.RemoveAll();
        }

        /// <summary>
        /// Indexer to return IPlayer object
        /// </summary>
        /// <param name="id">Guid of a Player</param>
        /// <returns></returns>
        public IPlayer this[Guid id]
        {
            get
            {
                return this._players.Where(p => p.Id.Equals(id)).FirstOrDefault();
            }            
        }

        /// <summary>
        /// To get the first player of the match
        /// </summary>
        public IPlayer FirstPlayer
        {
            get
            {
                Player player = _players[FIRST_PLAYER];
                if (player != null)
                    player.Opponent = _players[SECOND_PLAYER];

                return player;
            }

            set { _players[FIRST_PLAYER] = AssignPlayer(value); }
        } 

        /// <summary>
        /// To get the Second player of the match
        /// </summary>
        public IPlayer SecondPlayer
        {
            get
            {
                Player player = _players[SECOND_PLAYER];

                if (player != null)
                    player.Opponent = _players[FIRST_PLAYER];

                return player;
            }

            set { _players[SECOND_PLAYER] = AssignPlayer(value); }
        }

        /// <summary>
        /// Set the current server
        /// </summary>
        public IPlayer Server { get; set; }
 
        /// <summary>
        /// Validate and assign the event actions for the player
        /// </summary>
        /// <param name="player">Player to validate</param>
        /// <returns>Returns valid player</returns>
        private Player AssignPlayer(IPlayer player)
        {
            // Check the duplication of the player, throw an exception when duplicate found
            var players = _players.Where(pl => pl != null && pl.Identity != null &&
                pl.Unique(player.Identity)).Select(entity => entity.Identity);

            if (players.Any())
                throw new DuplicateException(string.Format("Player {0}, {1} already selected as player, player's should be different!", player.Identity.SurName, player.Identity.FirstName));

            // Subscribe action events
            player.PointWin += OnPointWin;
            player.GamePointWin += OnGamePointWin;
            player.Deuce += OnDeuce;

            return (Player)player;
        }

        /// <summary>
        /// Remove the palyers
        /// </summary>
        public void RemoveAll()
        {
            // Remove the players from the list
            _players.Clear();

            _players.Add(null);
            _players.Add(null);
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

        /// <summary>
        /// Set the deuce for both the players
        /// </summary>
        /// <param name="winner">Winner player</param>
        private void OnDeuce(PlayerEntity winner)
        {
            // Set the Deuce point for both the players
            FirstPlayer.SetDeuce();
            SecondPlayer.SetDeuce();
        }
    }
}
