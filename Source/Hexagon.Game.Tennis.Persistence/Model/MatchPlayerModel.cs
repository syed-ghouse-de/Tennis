using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Match player model
    /// </summary>
    public class MatchPlayerModel : IModel
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchPlayerModel() { }

        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }

        public virtual MatchModel Match { get; set; }
        public virtual PlayerModel Player { get; set; }
    }
}
