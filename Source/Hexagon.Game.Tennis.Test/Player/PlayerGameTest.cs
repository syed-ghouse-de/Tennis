using Hexagon.Game.Framework.Exceptions;
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
    /// Player Game unit test cases
    /// </summary>
    public class PlayerGameTest : BaseTest
    {
        // Member variable objects for persistence mock
        private Mock<IMatchPersistenceService> _matchPersistenceMock;
        private Mock<IPlayerPersistenceService> _playerPersistenceMock;
        private Mock<IScorePersistenceService> _scorePersistenceMock;
        private IMatch _match;
       
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerGameTest()
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
        /// Playe game with first inprogress game
        /// </summary>
        [Fact]
        public void PlayGameWithFirstInprogressGame()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();

            // Set score boundry checks
            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).Status);

            Assert.Equal(1, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(0).Status);

            // Server and player verification
            Assert.Equal(match.Players.Server.Identity.FirstName,
                match.Score.GetSet(0).GetGame(0).Server.FirstName);
            Assert.Equal(match.Players.FirstPlayer.Identity.FirstName,
                match.Score.GetSet(0).GetGame(0).Server.FirstName);

            firstPlayer.Win();

            // Plyaer current point checks
            Assert.Equal(firstPlayer.Point.Point, PlayerPoint.GamePoint);
            Assert.Equal(secondPlayer.Point.Point, PlayerPoint.Love);

            // Score total sets and Set status
            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).Status);

            // Total games and game status
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status); 

            // Current set and game checks
            Assert.Equal(Status.InProgress, match.Score.CurrentSet.Status);
            Assert.Equal(Status.InProgress, match.Score.CurrentGame.Status);

            // Game server boundry checks
            Assert.Equal(match.Players.Server.Identity.FirstName,
                match.Score.GetSet(0).GetGame(1).Server.FirstName);
            Assert.Equal(match.Players.SecondPlayer.Identity.FirstName,
                match.Score.GetSet(0).GetGame(1).Server.FirstName);

            var points = string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString()));

            // Player game points boundery checks
            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        ///  Player points for a game
        /// </summary>
        [Fact]
        public void PlayGameWithPlayerPoints()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            secondPlayer.Win();         // 15   15
            firstPlayer.Win();          // 30   15
            firstPlayer.Win();          // 40   15
            secondPlayer.Win();         // 40   30  

            // Players game points boundery checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game with Player1 game point
        /// </summary>
        [Fact]
        public void PlayGameWithFirstPlayerGamePoint()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            secondPlayer.Win();         // 15   15
            firstPlayer.Win();          // 30   15
            firstPlayer.Win();          // 40   15
            secondPlayer.Win();         // 40   30  
            firstPlayer.Win();          // Game 30

            // Plyer game point boundry checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Thirty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game with Player1 straight game point win
        /// </summary>
        [Fact]
        public void PlayGameWithFirstPlayerStraightGamePointWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            firstPlayer.Win();          // 30   0
            firstPlayer.Win();          // 40   0    
            firstPlayer.Win();          // Game 0

            // Player game point boundry checks
            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game with Player2 straight game win
        /// </summary>
        [Fact]
        public void PlayGameWithSecondPlayerStraightGamePointWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            secondPlayer.Win();          // 0   15
            secondPlayer.Win();          // 0   30
            secondPlayer.Win();          // 0   40    
            secondPlayer.Win();          // 0   Game

            // Player game point bounder checks
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            // Player current stats of Set and Game boundery checks
            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        /// <summary>
        /// Player game with Deuce point
        /// </summary>
        [Fact]
        public void PlayGameWithDeuce()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            secondPlayer.Win();         // 15   15
            firstPlayer.Win();          // 30   15
            firstPlayer.Win();          // 40   15
            secondPlayer.Win();         // 40   30
            secondPlayer.Win();         // 40   40      Deuce

            // Player game point boundry checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game when Player2 is in Advantage
        /// </summary>
        [Fact]
        public void PlayGameWithSecondPlayerAdvantage()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            secondPlayer.Win();         // 15   15
            firstPlayer.Win();          // 30   15
            firstPlayer.Win();          // 40   15
            secondPlayer.Win();         // 40   30
            secondPlayer.Win();         // 40   40          Deuce
            secondPlayer.Win();         // 40   Advantage

            // Player game point boundry checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Advantage", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game when Player1 is in Advantage
        /// </summary>
        [Fact]
        public void PlayGameWithFirstPlayerAdvantage()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15            0
            secondPlayer.Win();         // 15           15
            firstPlayer.Win();          // 30           15
            firstPlayer.Win();          // 40           15
            secondPlayer.Win();         // 40           30
            secondPlayer.Win();         // 40           40          Deuce            
            firstPlayer.Win();          // Advantage    40

            // Player game point boundry checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player game with multiple Deuce and Advantages
        /// </summary>
        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantages()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15            0
            secondPlayer.Win();         // 15           15
            firstPlayer.Win();          // 30           15
            firstPlayer.Win();          // 40           15
            secondPlayer.Win();         // 40           30
            secondPlayer.Win();         // 40           40      Deuce            
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40      Deuce
            secondPlayer.Win();         // 40           Advantage
            firstPlayer.Win();          // 40           40      Deuce
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40

            // Player game point boundry checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        /// <summary>
        /// Player1 game point win with multiple Deuce & Advantages
        /// </summary>
        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantagesFirstPlayerWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15            0
            secondPlayer.Win();         // 15           15
            firstPlayer.Win();          // 30           15
            firstPlayer.Win();          // 40           15
            secondPlayer.Win();         // 40           30
            secondPlayer.Win();         // 40           40      Deuce            
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40      Deuce
            secondPlayer.Win();         // 40           40
            firstPlayer.Win();          // 40           40      Deuce
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40
            firstPlayer.Win();          // Advantage    40
            firstPlayer.Win();          // GamePoint    40

            // Player1 game point boundery checks
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty-Advantage-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            // Set and Game checks after game point win
            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        /// <summary>
        /// Player2 game point win with multiple Deuce & Advantages
        /// </summary>
        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantagesSecondPlayerWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15            0
            secondPlayer.Win();         // 15           15
            firstPlayer.Win();          // 30           15
            firstPlayer.Win();          // 40           15
            secondPlayer.Win();         // 40           30
            secondPlayer.Win();         // 40           40      Deuce            
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40      Deuce
            secondPlayer.Win();         // 40           40
            firstPlayer.Win();          // 40           40      Deuce
            firstPlayer.Win();          // Advantage    40
            secondPlayer.Win();         // 40           40
            secondPlayer.Win();         // 40           Advantage
            secondPlayer.Win();         // 40           GamePoint

            // Players game point boundry checks after game point win
            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            // Boundery checks for Set & Game status
            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        /// <summary>
        /// Exception is expected when get the current Set details 
        /// without start of the match
        /// </summary>
        [Fact]
        public void GetCurrentSetDetailsWithoutStartingOfMatch()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;            

            // Exception is expected when trys to get the current score before start of the match
            Exception exception = Assert.Throws<Exception>(() => match.Score.CurrentSet);
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Exception is expected when Game details 
        /// is requested without start of the match
        /// </summary>
        [Fact]
        public void GetCurrentGameDetailsWithoutStartingOfMatch()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;

            // Execption is expected when trys to get the game status before starting of the match
            Exception exception = Assert.Throws<Exception>(() => match.Score.CurrentGame);
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Exception is expected when get the Set details without starting of the match
        /// </summary>
        [Fact]
        public void GetSetDetailsWithoutStartingOfMatch()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;

            // Exception is expected when trys to get the particular Set Details before starting of the match
            Exception exception = Assert.Throws<Exception>(() => match.Score.GetSet(0));
            Assert.NotNull(exception);
        }
    }
}


