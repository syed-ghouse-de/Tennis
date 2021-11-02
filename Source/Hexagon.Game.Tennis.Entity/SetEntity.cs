using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Class to manage Set
    /// </summary>
    public class SetEntity : BaseEntity
    {
        public PlayerEntity WonBy { get; set; }
        public Status Status { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }     
        public List<GameEntity> Games { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SetEntity()
        {
            Init();   
        }
     
        /// <summary>
        /// Get the total number of Games
        /// </summary>
        public int TotalGames { get { return Games.Count; } }

        /// <summary>
        /// Get the Game details for the specicific set number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public GameEntity GetGame(int number)
        {
            return Games[number];
        }

        /// <summary>
        /// Initialze default values 
        /// </summary>
        private void Init()
        {
            Id = Guid.NewGuid();

            Games = new List<GameEntity>();
            Status = Status.InProgress;
        }
    }
}
