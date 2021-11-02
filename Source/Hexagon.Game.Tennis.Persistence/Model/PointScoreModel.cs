using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Point score model
    /// </summary>
    public class PointScoreModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PointScoreModel() { }

        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PointId { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual GameScoreModel Game { get; set; }
        public virtual PlayerModel Player { get; set; }
        public virtual PointLookupModel Point { get; set; }
    }
}
