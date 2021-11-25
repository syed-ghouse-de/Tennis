using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Match model
    /// </summary>
    public class MatchModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchModel()
        {
            MatchPlayer = new HashSet<MatchPlayerModel>();
            SetScore = new HashSet<SetScoreModel>();
        }        

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Court { get; set; }
        public Guid? WonBy { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int StatusId { get; set; }

        public virtual StatusLookupModel Status { get; set; }
        public virtual ICollection<MatchPlayerModel> MatchPlayer { get; set; }
        public virtual ICollection<SetScoreModel> SetScore { get; set; }
        public virtual PlayerModel WonByNavigation { get; set; }
    }
}
