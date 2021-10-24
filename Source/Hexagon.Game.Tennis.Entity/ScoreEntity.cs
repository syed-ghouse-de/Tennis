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
        public List<SetEntity> Sets { get; set; }
        public SetEntity CurrentSet { get; }                       // Current Set Score
        public GameEntity CurrentGame { get; }                     // Current Game Score   

        /// <summary>
        /// Get the total number of Sets
        /// </summary>
        public int TotalSets { get { return Sets.Count; } }

        /// <summary>
        /// Get the set details for the specicific set number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SetEntity GetSet(int number)
        {
            return Sets[number];
        }
    }
}
