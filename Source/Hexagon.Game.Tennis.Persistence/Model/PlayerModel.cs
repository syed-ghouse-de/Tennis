using Hexagon.Game.Framework.Service.Persistence;
using System;
using System.Collections.Generic;

namespace Hexagon.Game.Tennis.Persistence.Model
{
    /// <summary>
    /// Player model
    /// </summary>
    public class PlayerModel : IModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerModel()
        {
            // Initialize association properties
            GameScoreServedByNavigation = new HashSet<GameScoreModel>();
            GameScoreWonByNavigation = new HashSet<GameScoreModel>();
            MatchPlayer = new HashSet<MatchPlayerModel>();
            PointScore = new HashSet<PointScoreModel>();
            SetScore = new HashSet<SetScoreModel>();
            Match = new HashSet<MatchModel>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Club { get; set; }

        public virtual ICollection<GameScoreModel> GameScoreServedByNavigation { get; set; }
        public virtual ICollection<GameScoreModel> GameScoreWonByNavigation { get; set; }
        public virtual ICollection<MatchPlayerModel> MatchPlayer { get; set; }
        public virtual ICollection<PointScoreModel> PointScore { get; set; }
        public virtual ICollection<SetScoreModel> SetScore { get; set; }
        public virtual ICollection<MatchModel> Match { get; set; }
    }
}
