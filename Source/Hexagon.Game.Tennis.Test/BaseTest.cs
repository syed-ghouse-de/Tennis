using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Test
{
    public class BaseTest
    {
        /// <summary>
        /// Add two players for unit testing
        /// </summary>
        /// <returns></returns>
        public Players AddPlayers()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.FirstPlayer = first;
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.SecondPlayer = second;

            return players;
        }
    }
}
