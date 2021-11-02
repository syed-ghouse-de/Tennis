using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Set score model
    /// </summary>
    public class SetScoreModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SetScoreModel()
        {
            // Initilize associated properties
            GameScore = new HashSet<GameScoreModel>();
        }

        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid? WonBy { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int StatusId { get; set; }

        public virtual MatchModel Match { get; set; }
        public virtual StatusLookupModel Status { get; set; }
        public virtual PlayerModel WonByNavigation { get; set; }
        public virtual ICollection<GameScoreModel> GameScore { get; set; }
    }
}
