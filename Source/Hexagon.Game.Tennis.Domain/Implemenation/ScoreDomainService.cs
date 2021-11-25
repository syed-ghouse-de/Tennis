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
                GameWon(score.CurrentGame, winner);

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
                    SetWon(score.CurrentSet, winner);  

                    // If best of 3 sets then match is completed
                    // and do not continue further
                    var winPlayerSets = score.Sets.Where(
                        s => s.WonBy != null && s.WonBy.Id.Equals(winner.Id)).Count();
                    var bestOfSets = Math.Ceiling((double)match.BestOfSets / 2) +
                        ((match.BestOfSets % 2) == 0 ? 1 : 0);

                    if (winPlayerSets >= bestOfSets)
                    {
                        // Set the match to complete
                        MatchWon(match, winner);
                        return score;
                    }

                    // Careate an instance of new Set                             
                    score.Sets.Add(CreateNewSet(match));                  
                }

                // Create a an instance for new game and
                // add it to the current Set
                // Set the Game status a in progress                
                score.CurrentSet.Games.Add(CreateNewGame(score.CurrentSet, receiver));                
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
        /// <param name="match">Match information</param>
        /// <param name="score">Match score</param>
        /// <param name="server">Server of the game</param>
        /// <param name="winner">Point winner</param>
        /// <param name="looser">Point looser</param>
        /// <param name="point">Winning point</param>
        /// <returns>Returns score details</returns>
        public ScoreEntity PointWin(MatchEntity match, ScoreEntity score, 
            PlayerEntity server, PlayerEntity winner, PlayerEntity looser, PlayerPoint point)
        {
            try
            {
                // Create a in progress Set if not present
                if (score.TotalSets.Equals(0))                          
                    score.Sets.Add(CreateNewSet(match));
                
                // Create a in progess Game is not present
                if (score.CurrentSet.TotalGames.Equals(0))
                    score.CurrentSet.Games.Add(CreateNewGame(score.CurrentSet, server));

                // Create winner point details
                var winnerEntity = CreateNewPoint(winner, point);

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

                // Create looser point details 
                var looserEntity = CreateNewPoint(looser, looserPoint);   

                // Add winner points in to current game and database
                score.CurrentGame.PlayerPoints.Add(winnerEntity);
                _scorePersistenceService.AddPoint(score.CurrentGame.Id, winnerEntity);

                // Add looser points in to current game and database
                score.CurrentGame.PlayerPoints.Add(looserEntity);
                _scorePersistenceService.AddPoint(score.CurrentGame.Id, looserEntity);                
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

        /// <summary>
        /// Create player point details
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="point">Point of the Player</param>
        /// <returns></returns>
        private PlayerPointEntity CreateNewPoint(PlayerEntity player, PlayerPoint point)
        {
            // Prepare player point details
            return new PlayerPointEntity()
            {
                Id = Guid.NewGuid(),

                Player = new PlayerEntity(player),
                Point = point,
                UpdatedOn = DateTime.UtcNow                     // Update current date and time in UTC
            };            
        }

        /// <summary>
        /// Create and add new game in to database
        /// </summary>
        /// <param name="set">Set details to be added</param>
        /// <param name="server">Server of the game</param>
        /// <returns>Returns new game</returns>
        private GameEntity CreateNewGame(SetEntity set, PlayerEntity server)
        {
            // Create new Game and initialize default properties
            var newGame = new GameEntity()
            {
                Id = Guid.NewGuid(),

                StartedOn = DateTime.UtcNow,
                Server = new PlayerEntity(server),
                Status = Status.InProgress
            };

            // Add game details in to database
            _scorePersistenceService.AddGame(set.Id, newGame);

            // Return updated Game
            return newGame;
        }

        /// <summary>
        /// Create and add new set in to database
        /// </summary>
        /// <param name="match">Match information</param>
        /// <returns>Returns SetEntity</returns>
        private SetEntity CreateNewSet(MatchEntity match)
        {
            // Create new set and initialize default properties
            var newSet = new SetEntity()
            {
                Id = Guid.NewGuid(),
                
                StartedOn = DateTime.UtcNow,
                Status = Status.InProgress
            };

            // Add set details in to database
            _scorePersistenceService.AddSet(match.Id, newSet);

            // Return updated Set
            return newSet;
        }

        /// <summary>
        /// Update the completed Match details
        /// </summary>
        /// <param name="match">Match</param>
        /// <param name="winner">Winner of the match</param>
        private void MatchWon(MatchEntity match, PlayerEntity winner)
        {
            // Player who won the match 
            match.WonBy = winner;

            // Mark the set to complete
            match.CompletedOn = DateTime.UtcNow;
            match.Status = Status.Completed;

            // Update compeled game in to database
            _matchPersistenceService.UpdateMatch(match);
        }

        /// <summary>
        /// Update the completed set details
        /// </summary>
        /// <param name="set">Set</param>
        /// <param name="winner">Winner of the set</param>
        private void SetWon(SetEntity set, PlayerEntity winner)
        {
            // Player who won the set 
            set.WonBy = new PlayerEntity(winner);

            // Mark the set to complete
            set.Status = Status.Completed;
            set.CompletedOn = DateTime.UtcNow;

            // Update compeled game in to database
            _scorePersistenceService.UpdateSet(set);
        }

        /// <summary>
        /// Update the completed game details
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="winner">Winner of the game</param>
        private void GameWon(GameEntity game, PlayerEntity winner)
        {
            // Player who won the game 
            game.WonBy = new PlayerEntity(winner);

            // Mark the game to complete
            game.Status = Status.Completed;
            game.CompletedOn = DateTime.UtcNow;

            // Update compeled game in to database
            _scorePersistenceService.UpdateGame(game);
        }
    }
}
