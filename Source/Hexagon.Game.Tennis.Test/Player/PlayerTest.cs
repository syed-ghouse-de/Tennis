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
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe" };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex"};
            players.Add(second);

            Assert.True(players.Count.Equals(2));
            Assert.Equal(players.FirstPlayer, first);
            Assert.Equal(players.SecondPlayer, second);
        }
        
        /// <summary>
        /// Test case to throw excpetion when more than two players gets added
        /// </summary>
        [Fact]     
        public void AddMoreThenTwoPlayersThrowExcpetion()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe" };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex" };
            players.Add(second);

            Player third = new Player { Id = Guid.NewGuid(), FirstName = "Bernherd", SurName = "Ritter" };
            Exception exception = Assert.Throws<InvalidOperationException>(() => players.Add(third));

            Assert.NotNull(exception);
        }

        /// <summary>
        /// Test case to throw exception when trys to add duplicate player
        /// </summary>
        [Fact]
        public void AddingDuplicatePlayerThrowExcpetion()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe" , DateOfBirth = new DateTime(1993, 11, 7)};
            players.Add(first);

            Exception exception = Assert.Throws<DuplicateException>(() => players.Add(first));
            Assert.NotNull(exception);
        }
    }
}
