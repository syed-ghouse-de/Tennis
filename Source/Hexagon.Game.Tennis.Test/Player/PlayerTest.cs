using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Tennis;
using Xunit;

namespace Hexagon.Game.Tennis.Test
{
    /// <summary>
    /// Player unit test class
    /// </summary>
    public class PlayerTest 
    {
        /// <summary>
        /// Player test constructor
        /// </summary>
        public PlayerTest()
        { }

        /// <summary>
        /// Test case for adding players
        /// </summary>
        [Fact]
        public void AddPlayers()
        {
            // Players details
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.FirstPlayer = first;
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.SecondPlayer = second;

            // Players boundry checks            
            Assert.True(players.FirstPlayer.Identity.Unique(first));
            Assert.True(players.SecondPlayer.Identity.Unique(second));
        }         

        /// <summary>
        /// Test case to throw exception when trys to add duplicate player
        /// </summary>
        [Fact]
        public void AddingDuplicatePlayerThrowExcpetion()
        {
            // Players details
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe" , DateOfBirth = new DateTime(1993, 11, 7)};
            players.FirstPlayer = first;

            // Exception is expected when duplicate player is added
            Exception exception = Assert.Throws<DuplicateException>(() => players.SecondPlayer = first);
            Assert.NotNull(exception);
        }        
    }
}
