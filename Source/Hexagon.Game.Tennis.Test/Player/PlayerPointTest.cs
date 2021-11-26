using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Entity;
using Moq;
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
    public class PlayerPointTest :  BaseTest
    {
        // Member variable objects for persistence mock
        private Mock<IMatchPersistenceService> _matchPersistenceMock;
        private Mock<IPlayerPersistenceService> _playerPersistenceMock;
        private Mock<IScorePersistenceService> _scorePersistenceMock;
        private IMatch _match;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerPointTest()
        {
            // Mock the match, player and score persistence serivices
            _matchPersistenceMock = new Mock<IMatchPersistenceService>();
            _playerPersistenceMock = new Mock<IPlayerPersistenceService>();
            _scorePersistenceMock = new Mock<IScorePersistenceService>();

            // Create an instance of Match object by passing Mock objects
            _match = new Match(_matchPersistenceMock.Object,
                _playerPersistenceMock.Object, _scorePersistenceMock.Object);
        }

        /// <summary>
        /// Get the instance of Match object
        /// </summary>
        /// <returns>Return of type IMatch</returns>
        private IMatch GetMatch()
        {
            // Return the instance of Match object
            return _match;
        }

        /// <summary>
        /// Test case to calculate the players points
        /// </summary>
        [Fact]
        public void PlayerPoints()
        {
            Players players = AddPlayers(GetMatch());            

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
            Players players = AddPlayers(GetMatch());      

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
            Players players = AddPlayers(GetMatch());    

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
            Players players = AddPlayers(GetMatch());
            
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
            Players players = AddPlayers(GetMatch());     

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
            Players players = AddPlayers(GetMatch());          

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
            Players players = AddPlayers(GetMatch());

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
