using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Game score model
    /// </summary>
    public class GameScoreModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameScoreModel()
        {
            PointScore = new HashSet<PointScoreModel>();
        }

        public Guid Id { get; set; }
        public Guid SetId { get; set; }
        public Guid ServedBy { get; set; }
        public Guid? WonBy { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int StatusId { get; set; }

        public virtual PlayerModel ServedByNavigation { get; set; }
        public virtual SetScoreModel Set { get; set; }
        public virtual StatusLookupModel Status { get; set; }
        public virtual PlayerModel WonByNavigation { get; set; }
        public virtual ICollection<PointScoreModel> PointScore { get; set; }
    }
}
