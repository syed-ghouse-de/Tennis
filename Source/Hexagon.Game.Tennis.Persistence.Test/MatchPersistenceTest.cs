using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Model;
using Hexagon.Game.Tennis.Persistence.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Test
{
    public class MatchPersistenceTest
    {
       public static void AddMatch()
        {
            IMatchPersistenceService matchPersistence = 
                PersistenceService.Instance<MatchPersistenceService>();

            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "Fisrt Match", StartedOn = DateTime.UtcNow, Status = Status.InProgress};
            matchPersistence.AddMatch(match);               
        }

        public static void GetMatch()
        {
            IMatchPersistenceService matchPersistence =
                PersistenceService.Instance<MatchPersistenceService>();
            
            var match = matchPersistence.GetMatch(new Guid("85F6BBA9-0692-48DD-B86F-4A1B1C58C7AC"));
        }

        public static void AddSet()
        {
            IScorePersistenceService matchPersistence =
                PersistenceService.Instance<ScorePersistenceService>();

            var entity = new SetEntity();           
            entity.StartedOn = DateTime.UtcNow;
            entity.Status = Status.InProgress;

            matchPersistence.AddSet(new Guid("85F6BBA9-0692-48DD-B86F-4A1B1C58C7AC"), entity);
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                var game = new GameEntity();                
                game.Server = new PlayerEntity() { Id = new Guid("30FCADA5-706B-44B1-B1DC-8BF835B7AF20") };
                game.WonBy = new PlayerEntity() { Id = new Guid("72611B67-1D49-4707-83FA-6AF87E46BD89") };
                game.StartedOn = DateTime.UtcNow;
                game.Status = Status.InProgress;

                matchPersistence.AddGame(entity.Id, game);

                for (int pointCount = 0; pointCount < 4; pointCount++)
                {
                    var point = new PlayerPointEntity();
                    point.Id = Guid.NewGuid();                    
                    point.Player = new PlayerEntity() { Id = new Guid("30FCADA5-706B-44B1-B1DC-8BF835B7AF20")};
                    point.Point = PlayerPoint.Advantage;
                    point.UpdatedOn = DateTime.UtcNow;

                    matchPersistence.AddPoint(game.Id, point);
                }
            }
        }

        public static void UpdateSet()
        {
            IMatchPersistenceService matchService = new MatchPersistenceService();
            IScorePersistenceService scoreService =
                PersistenceService.Instance<ScorePersistenceService>();

            var match = matchService.GetMatch(new Guid("85F6BBA9-0692-48DD-B86F-4A1B1C58C7AC"));
            var set = match.Sets.Where(s => s.Id.Equals(new Guid("9E31EC8D-CC00-4352-A1EE-75632A9B00F7"))).FirstOrDefault();
            set.Status = Status.NoStarted;
            set.WonBy = new PlayerEntity() { Id = new Guid("30FCADA5-706B-44B1-B1DC-8BF835B7AF20") };
            scoreService.UpdateSet(set);        
        }
    }
}
