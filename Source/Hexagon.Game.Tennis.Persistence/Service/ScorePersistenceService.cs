using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Service
{
    /// <summary>
    /// Score persistence operations
    /// </summary>
    public class ScorePersistenceService : BasePersistenceService, IScorePersistenceService
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScorePersistenceService() { }

        /// <summary>
        /// Object Instance
        /// </summary>
        /// <returns>Returns an insance of ScorePersistenceService</returns>
        public IPersistenceService Instance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a game in to database
        /// </summary>
        /// <param name="setId">Add a game for a specified Set</param>
        /// <param name="game">Game to add into database</param>
        public void AddGame(Guid setId, GameEntity game)
        {
            try
            {
                // Prepare a game model
                GameScoreModel model = new GameScoreModel()
                {
                    Id = game.Id,
                    SetId = setId,
                    ServedBy = game.Server.Id,                    
                    StartedOn = game.StartedOn,                    
                    StatusId = (int)game.Status
                };

                // Add an Game record in to database
                Repository<GameScoreModel>().Add(model);
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }

        /// <summary>
        /// Add a point into database
        /// </summary>
        /// <param name="gameId">Add a point for a specified game</param>
        /// <param name="point">Add a point into database</param>
        public void AddPoint(Guid gameId, PlayerPointEntity point)
        {
            try
            {
                // Prepare a game model
                PointScoreModel model = new PointScoreModel()
                {
                    Id = point.Id,
                    GameId = gameId,
                    PlayerId = point.Player.Id,
                    PointId = (int)point.Point,
                    UpdatedOn = point.UpdatedOn
                };

                // Add an Game record in to database
                Repository<PointScoreModel>().Add(model);
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }

        /// <summary>
        /// Add Set into database
        /// </summary>
        /// <param name="matchId">Add a Set for specified Match</param>
        /// <param name="set">Add as Set into database</param>
        public void AddSet(Guid matchId, SetEntity set)
        {
            try
            {
                // Prepare a game model
                SetScoreModel model = new SetScoreModel()
                {
                    Id = set.Id,
                    MatchId = matchId,                    
                    StartedOn = set.StartedOn,                    
                    StatusId = (int)set.Status
                };

                // Add an Game record in to database
                Repository<SetScoreModel>().Add(model);
            }
            catch (Exception exception)
            {
                // Throw a excpetion
                throw new PersistenceServiceException(exception.Message);
            }
        }

        /// <summary>
        /// Update a Game into database
        /// </summary>
        /// <param name="game">Game to update in database</param>
        public void UpdateGame(GameEntity game)
        {
            try
            {
                // Find the Game for the specified id
                var model = Repository<GameScoreModel>().Find(game.Id);
                if (model != null)
                {
                    // Update the fields of Game model
                    model.WonBy = game.WonBy.Id;
                    model.CompletedOn = game.CompletedOn;
                    model.StatusId = (int)game.Status;

                    // Update Set record in to database
                    Repository<GameScoreModel>().Update(model);
                }
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }

        /// <summary>
        /// Update Set
        /// </summary>
        /// <param name="set">Set to update in the database</param>
        public void UpdateSet(SetEntity set)
        {
            try
            {
                // Find the Set for the specified id
                var model = Repository<SetScoreModel>().Find(set.Id);
                if (model != null)
                {
                    // Update the fields of Set model
                    model.WonBy = set.WonBy.Id;
                    model.CompletedOn = set.CompletedOn;
                    model.StatusId = (int)set.Status;

                    // Update Set record in to database
                    Repository<SetScoreModel>().Update(model);
                }
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }
    }
}
