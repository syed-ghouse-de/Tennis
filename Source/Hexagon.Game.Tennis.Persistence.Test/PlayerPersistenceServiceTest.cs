using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Persistence.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Test
{
    public class PlayerPersistenceServiceTest
    {
        public static void GetAllPlayers()
        {
            IPlayerPersistenceService playerPersistence =
                PersistenceService.Instance<PlayerPersistenceService>();

            var players = playerPersistence.GetPlayers();
        }
    }
}
