using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Service;

namespace Hexagon.Game.Tennis.Domain.Service.Implementation
{
    /// <summary>
    /// Business logic class for Score
    /// </summary>
    public class ScoreDomainService : IScoreDomainService
    {
        // Member variable for persistence service
        private IMatchPersistenceService _matchPersistenceService;
        private IScorePersistenceService _scorePersistenceService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScoreDomainService(IMatchPersistenceService matchPersistenceService,
            IScorePersistenceService scorePersistenceService)
        {
            // Initialize persistence services
            _matchPersistenceService = matchPersistenceService;
            _scorePersistenceService = scorePersistenceService;
        }

        /// <summary>
        /// Service method to calculate and update player game point win 
        /// </summary>
        /// <param name="match">Match information</param>
        /// <param name="score">Match score</param>
        /// <param name="receiver">Receiver player</param>
        /// <param name="winner">Winning player</param>
        /// <returns>Returns score details</returns>
        public ScoreEntity GamePointWin(MatchEntity match, 
            ScoreEntity score, PlayerEntity receiver, PlayerEntity winner)
        {
            try
            {
                // Set the current game to complete
                score.CurrentGame.Status = Status.Completed;
                score.CurrentGame.WonBy = new PlayerEntity(winner);
                score.CurrentGame.CompletedOn = DateTime.UtcNow;                                              

                // Get the total games won by each player
                var winPlayerGames = score.CurrentSet.Games.Where(
                    g => g.WonBy != null && g.WonBy.Id.Equals(winner.Id)).Count();
                var opponentPlayerGames = score.CurrentSet.Games.Where(
                    g => g.WonBy != null && !g.WonBy.Id.Equals(winner.Id)).Count();

                // If total games are greater than 6 and 
                // the difference should be greather equal to 2
                if (winPlayerGames >= 6 && (winPlayerGames - opponentPlayerGames) >= 2)
                {
                    // Set the current Set to complete
                    score.CurrentSet.Status = Status.Completed;
                    score.CurrentSet.WonBy = new PlayerEntity(winner);
                    score.CurrentSet.CompletedOn = DateTime.UtcNow; 

                    // If best of 3 sets then match is completed
                    // and do not continue further
                    var winPlayerSets = score.Sets.Where(
                        s => s.WonBy != null && s.WonBy.Id.Equals(winner.Id)).Count();
                    var bestOfSets = Math.Ceiling((double)match.BestOfSets / 2) +
                        ((match.BestOfSets % 2) == 0 ? 1 : 0);

                    if (winPlayerSets >= bestOfSets)
                    {
                        match.WonBy = winner;
                        match.CompletedOn = DateTime.UtcNow;
                        match.Status = Status.Completed;
                        // TODO: Update Match in the database                           

                        return score;
                    }

                    // Careate an instance of new Set
                    var setEntity = new SetEntity()
                    {
                        Id = Guid.NewGuid(),                        
                        Games = new List<GameEntity>(),
                        Status = Status.InProgress
                    };
                    
                    score.Sets.Add(setEntity);                  
                }

                // Create a an instance for new game and
                // add it to the current Set
                var newGameEntity = new GameEntity()
                {
                    Id = Guid.NewGuid(), 
                    Server = new PlayerEntity(receiver),                // Set the Game server as reciver
                    Status = Status.InProgress                          // Set the Game status a in progress
                };

                score.CurrentSet.Games.Add(newGameEntity);
                
                // TODO: Dabasebase operations
                // 1. Update completed Game into the database
                // 2. Update Completed Set into the database
                // 3. Add new Set & Game into the database                
            }
            catch(Exception exception)
            {
                throw new DomainServiceException(exception.Message);
            }

            return score;
        }

        /// <summary>
        /// Service method to calculate and update player point win
        /// </summary>
        /// <param name="score">Match score</param>
        /// <param name="server">Server of the game</param>
        /// <param name="winner">Point winner</param>
        /// <param name="looser">Point looser</param>
        /// <param name="point">Winning point</param>
        /// <returns>Returns score details</returns>
        public ScoreEntity PointWin(ScoreEntity score, PlayerEntity server,
            PlayerEntity winner, PlayerEntity looser, PlayerPoint point)
        {
            try
            {
                // Create a in progress Set if not present
                if (score.TotalSets.Equals(0))
                    score.Sets.Add(new SetEntity());

                // Create a in progess Game is not present
                if (score.CurrentSet.TotalGames.Equals(0))
                    score.CurrentSet.Games.Add(new GameEntity() { Server = new PlayerEntity(server) });

                // Prepeare winner point details
                var winnerEntity = new PlayerPointEntity()
                {
                    Player = new PlayerEntity(winner),
                    Point = point,
                    UpdatedOn = DateTime.UtcNow                     // Update current date and time in UTC
                };

                // Get the looser point to update
                var looserPoint = PlayerPoint.Love;                 // Initiaze default point to LOVE
                var looserPoints = score.CurrentGame.PlayerPoints
                    .Where(pp => pp.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn);

                // Get the point of the looser
                if (looserPoints.Any())
                {
                    looserPoint = looserPoints.Last().Point;
                    if (looserPoint.Equals(PlayerPoint.Advantage))
                        looserPoint = PlayerPoint.Forty;
                }

                // Prepare looser point details 
                var looserEntity = new PlayerPointEntity()
                {
                    Player = new PlayerEntity(looser),
                    Point = looserPoint, 
                    UpdatedOn = DateTime.UtcNow                     // Update current date and time in UTC
                };

                // Add pints of both winner and looser players
                score.CurrentGame.PlayerPoints.Add(winnerEntity);
                score.CurrentGame.PlayerPoints.Add(looserEntity);

                // TODO:
                // Add winner & looser point details in to database
            }
            catch (Exception exception)
            {
                throw new DomainServiceException(exception.Message);
            }

            return score;
        }

        /// <summary>
        /// Get the score of current match
        /// </summary>
        /// <param name="match">Match information</param>
        /// <param name="server">Name of the server player</param>
        /// <returns>Returns score details</returns>
        public ScoreEntity GetMatchScore(MatchEntity match, PlayerEntity server)
        {
            // TODO: Get the score details from database if mathch is alrady started

            ScoreEntity score = new ScoreEntity();
            if (score.TotalSets.Equals(1) && score.CurrentSet.TotalGames.Equals(1) &&
                score.CurrentGame.Status.Equals(Status.InProgress))
            {
                score.CurrentGame.Server = new PlayerEntity(server);
            }

            return score;
        }
    }
}
