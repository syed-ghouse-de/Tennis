using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Status lookup model
    /// </summary>
    public class StatusLookupModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public StatusLookupModel()
        {
            // Initialize associated properties
            GameScore = new HashSet<GameScoreModel>();
            Match = new HashSet<MatchModel>();
            SetScore = new HashSet<SetScoreModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GameScoreModel> GameScore { get; set; }
        public virtual ICollection<MatchModel> Match { get; set; }
        public virtual ICollection<SetScoreModel> SetScore { get; set; }
    }
}
