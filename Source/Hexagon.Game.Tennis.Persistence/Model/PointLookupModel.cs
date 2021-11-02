using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Point lookup model
    /// </summary>
    public class PointLookupModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PointLookupModel()
        {
            // Initialize associated properties
            PointScore = new HashSet<PointScoreModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PointScoreModel> PointScore { get; set; }
    }
}
