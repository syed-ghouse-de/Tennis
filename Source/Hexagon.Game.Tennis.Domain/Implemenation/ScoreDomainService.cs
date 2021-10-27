using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis.Domain.Service.Implemenation
{
    /// <summary>
    /// Business logic class for Score
    /// </summary>
    public class ScoreDomainService : IScoreDomainService
    {
        /// <summary>
        /// Service method to calculate and update player game point win 
        /// </summary>
        /// <param name="match">Match information</param>
        /// <param name="score">Match score</param>
        /// <param name="receiver">Receiver player</param>
        /// <param name="winPlayer">Winning player</param>
        /// <returns>Returns score details</returns>
        public ScoreEntity GamePointWin(MatchEntity match, 
            ScoreEntity score, PlayerEntity receiver, PlayerEntity winPlayer)
        {
            try
            {
                // Set the current game to complete
                score.CurrentGame.Status = Status.Completed;
                score.CurrentGame.WonBy = new PlayerEntity(winPlayer);      

                // Get the total games won by each player
                var winPlayerGames = score.CurrentSet.Games.Where(
                    g => g.WonBy.Id.Equals(winPlayer.Id)).Count();
                var opponentPlayerGames = score.CurrentSet.Games.Where(
                    g => !g.WonBy.Id.Equals(winPlayer.Id)).Count();

                // If total games are greater than 6 and 
                // the difference should be greather equal to 2
                if (winPlayerGames >= 6 && (winPlayerGames - opponentPlayerGames) >= 2)
                {
                    score.CurrentSet.Status = Status.Completed;
                    score.CurrentSet.WonBy = new PlayerEntity(winPlayer);

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
                    Server = new PlayerEntity(receiver),                 // Set the Game server as reciver
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
                    score.CurrentSet.Games.Add(new GameEntity() { Server = server });

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
