using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Context;
using Hexagon.Game.Tennis.Persistence.Model;

namespace Hexagon.Game.Tennis.Persistence.Service
{
    /// <summary>
    /// Class for match persistence service
    /// </summary>
    public class MatchPersistenceService : BasePersistenceService, IMatchPersistenceService
    { 
        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchPersistenceService() { }

        /// <summary>
        /// Object Instance
        /// </summary>
        /// <returns>Returns an insance of ScorePersistenceService</returns>
        public IPersistenceService Instance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert match details into database
        /// </summary>
        /// <param name="match">Match information to insert into database</param>
        public void AddMatch(MatchEntity match)
        {            
            try
            {
                // Prepare a match model
                MatchModel model = new MatchModel()
                {
                    Id = match.Id,
                    Name = match.Name,                                       
                    StatusId = (int)match.Status                     
                };

                // Add an Match record in to database
                Repository<MatchModel>().Add(model);
            }
            catch (Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);
            }
        }

        /// <summary>
        /// Update match details into database
        /// </summary>
        /// <param name="match">Match information to update into database</param>
        public void UpdateMatch(MatchEntity match)
        {
   
        }

        /// <summary>
        /// Get the match infomation
        /// </summary>
        /// <param name="id">Math id to get the match details</param>
        /// <returns></returns>
        public MatchEntity GetMatch(Guid id)
        {
            try
            {
                // Prepare a Match entity along with Set, Game & Point 
                var match = Repository<MatchModel>().Entities.Where(w => w.Id.Equals(id))
                    // Prepare match entity
                    .Select(m => new MatchEntity()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        StartedOn = m.StartedOn,
                        CompletedOn = m.CompletedOn,
                        Status = (Status)m.StatusId,
                        // Prepare Set entity
                        Sets = m.SetScore.Select(s => new SetEntity()
                        {
                            Id = s.Id,
                            StartedOn = s.StartedOn,
                            CompletedOn = s.CompletedOn,
                            Status = (Status)s.StatusId,
                            WonBy =  GetPlayer(s.WonByNavigation), 
                            // Prepare Game entity
                            Games = s.GameScore.Select(g => new GameEntity()
                            {
                                Id = g.Id,
                                StartedOn = g.StartedOn,
                                CompletedOn = g.CompletedOn,
                                Status = (Status)g.StatusId,
                                Server = GetPlayer(g.ServedByNavigation),
                                WonBy = GetPlayer(g.WonByNavigation),
                                // Prepare player points entity
                                PlayerPoints = g.PointScore.Select(pp => new PlayerPointEntity()
                                {
                                    Id = pp.Id,
                                    Player = GetPlayer(pp.Player),
                                    Point = (PlayerPoint) pp.PointId,
                                    UpdatedOn = pp.UpdatedOn
                                }).OrderBy(o => o.UpdatedOn).ToList()
                            }).OrderBy(o => o.StartedOn).ToList()
                        }).OrderBy(o => o.StartedOn).ToList()
                    }).ToList().FirstOrDefault();

                return match;
            }
            catch(Exception exception)
            {
                // Throw an exception
                throw new PersistenceServiceException(exception.Message);         
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private PlayerEntity GetPlayer(PlayerModel model)
        {
            // Check for null 
            if (model == null)
                return null;
            
            // Initializing player entity
            return new PlayerEntity()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                SurName = model.SurName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Club = model.Club
            };
        }
    }    
}
