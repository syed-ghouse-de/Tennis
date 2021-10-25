using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Data transfer class to manage Score
    /// </summary>
    public class ScoreEntity : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScoreEntity()
        {
            Init();
        }

        public List<SetEntity> Sets { get; set; }

        /// <summary>
        /// Property to get the current Set
        /// </summary>
        public SetEntity CurrentSet { get { return Sets[TotalSets - 1]; } }    
        
        /// <summary>
        /// Property to get the Current Game
        /// </summary>
        public GameEntity CurrentGame
        {
            get { return CurrentSet.Games[CurrentSet.TotalGames - 1]; }
        }                    

        /// <summary>
        /// Get the total number of Sets
        /// </summary>
        public int TotalSets { get { return Sets.Count; } }

        /// <summary>
        /// Get the set details for the specicific set number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SetEntity GetSet(int number) { return Sets[number]; }

        /// <summary>
        /// Initialze default values 
        /// </summary>
        private void Init()
        {
            Sets = new List<SetEntity>() { new SetEntity() };            
        }
    }
}
