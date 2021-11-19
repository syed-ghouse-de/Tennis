using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hexagon.Game.Tennis.Test
{
    /// <summary>
    /// Player point unit test cases
    /// </summary>
    public class PlayerPointTest
    {
        /// <summary>
        /// Test case to calculate the players points
        /// </summary>
        [Fact]
        public void PlayerPoints()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Fifteen, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);
           
            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Fifteen, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Thirty, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Test case for game point of the player
        /// </summary>
        [Fact]
        public void PlayerGamePoint()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.GamePoint, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Test case for Deuce point
        /// </summary>
        [Fact]
        public void PlayerDeucePoint()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();       
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Test case for Advantage point
        /// </summary>
        [Fact]
        public void PlayerAdvantagePoint()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Advantage, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Test case for multiple Deuce points with game point
        /// </summary>
        [Fact]
        public void PlayerMultipleDeucePointWithGamePoint()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Advantage, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Advantage, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Deuce, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Advantage, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();            
            Assert.Equal(PlayerPoint.Deuce, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.GamePoint, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Test case for game point of the player
        /// </summary>
        [Fact]
        public void PlayerCurrentPointForFirstPlayerWinScenario1()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Fifteen, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Thirty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.GamePoint, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.SecondPlayer.Win();
            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Fifteen, players.SecondPlayer.Point.Point);
        }

        /// <summary>
        /// Player current point after Player1 win
        /// </summary>
        [Fact]
        public void PlayerCurrentPointForFirstPlayerWinScenario2()
        {
            Players players = new Players();

            Player first = new Player { Id = Guid.NewGuid(), FirstName = "John", SurName = "Doe", LastName = "Last", DateOfBirth = new DateTime(1996, 11, 7) };
            players.Add(first);
            Player second = new Player { Id = Guid.NewGuid(), FirstName = "Smith", SurName = "Alex", LastName = "Last", DateOfBirth = new DateTime(1987, 11, 9) };
            players.Add(second);

            Assert.Equal(PlayerPoint.Love, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Fifteen, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Thirty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Forty, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.GamePoint, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);

            players.FirstPlayer.Win();
            Assert.Equal(PlayerPoint.Fifteen, players.FirstPlayer.Point.Point);
            Assert.Equal(PlayerPoint.Love, players.SecondPlayer.Point.Point);
        }
    }
}
