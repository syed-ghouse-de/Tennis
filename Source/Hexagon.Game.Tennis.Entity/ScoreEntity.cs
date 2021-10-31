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
        public SetEntity CurrentSet
        {
            get
            {
                // Throw exception if Set is not started
                if (!Sets.Any())
                    throw new Exception();

                return Sets[TotalSets - 1];
            }
        }    
        
        /// <summary>
        /// Property to get the Current Game
        /// </summary>
        public GameEntity CurrentGame
        {
            get
            {
                // Throw exception if games is not started
                if (!CurrentSet.Games.Any())
                    throw new Exception();

                return CurrentSet.Games[CurrentSet.TotalGames - 1];
            }
        }                    

        /// <summary>
        /// Get the total number of Sets
        /// </summary>
        public int TotalSets { get { return Sets.Count; } }

        /// <summary>
        /// Get the set details for the specicific set number, number is start with Zero index
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SetEntity GetSet(int number)
        {
            // Throw exception if Set is not started
            if (!Sets.Any())
                throw new Exception();

            return Sets[number];
        }

        /// <summary>
        /// Initialze default values 
        /// </summary>
        private void Init()
        {
            Sets = new List<SetEntity>();            
        }
    }
}
