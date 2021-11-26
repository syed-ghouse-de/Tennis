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
        public Players AddPlayers(IMatch match) 
        {        
            // Firt player
            Player first = new Player
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Last",
                DateOfBirth = new DateTime(1996, 11, 7)
            };            

            Player second = new Player
            {
                Id = Guid.NewGuid(),
                FirstName = "Smith",
                SurName = "Alex",
                LastName = "Last",
                DateOfBirth = new DateTime(1987, 11, 9)
            };

            // Initialize first & second players
            match.Players.FirstPlayer = first;
            match.Players.SecondPlayer = second;            
            match.Players.Server = first;

            return match.Players;
        }
    }
}
