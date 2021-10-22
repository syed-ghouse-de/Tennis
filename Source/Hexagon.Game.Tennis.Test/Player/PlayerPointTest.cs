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

            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Love);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Fifteen);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Thirty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);
           
            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Thirty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Fifteen);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Thirty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Thirty);

            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Forty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Thirty);
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

            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Love);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.GamePoint);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);
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

            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Love);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Forty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();       
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);
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

            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Love);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Forty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Advantage);
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

            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Love);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Forty);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Love);

            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Advantage);

            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);

            players.FirstPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Advantage);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Deuce);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.Advantage);

            players.SecondPlayer.Win();
            Assert.Equal(players.FirstPlayer.Point.Point, PlayerPoint.Deuce);
            Assert.Equal(players.SecondPlayer.Point.Point, PlayerPoint.GamePoint);
        }
    }
}
