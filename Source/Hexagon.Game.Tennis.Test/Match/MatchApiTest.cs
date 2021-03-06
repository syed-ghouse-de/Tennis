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
    /// Class to cover Match API functionalities
    /// </summary>
    public class MatchApiTest
    {
        // Member variable objects for persistence mock
        private Mock<IMatchPersistenceService> _matchPersistenceMock;
        private Mock<IPlayerPersistenceService> _playerPersistenceMock;
        private Mock<IScorePersistenceService> _scorePersistenceMock;
        private IMatch _match;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchApiTest()
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
        /// New match with valid players
        /// </summary>
        [Fact]
        public void NewMatchWithValidPlayersScenario1()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();
            
            // Boundry checks for the players
            Assert.Null(match.FirstPlayer);
            Assert.Null(match.Players.FirstPlayer);
            Assert.Null(match.SecondPlayer);
            Assert.Null(match.Players.SecondPlayer);

            // Initilalize match players
            match.Players.FirstPlayer = new Player(first);
            match.Players.SecondPlayer= new Player(second);
            match.NewMatch("First Match");

            // Boundry checks for first player
            Assert.NotNull(match.FirstPlayer);
            Assert.NotNull(match.Players.FirstPlayer);
            Assert.Equal(first.FirstName, match.Players.FirstPlayer.Identity.FirstName);
            Assert.Equal(first.LastName, match.Players.FirstPlayer.Identity.LastName);
            Assert.Equal(first.SurName, match.Players.FirstPlayer.Identity.SurName);
            Assert.Equal(first.DateOfBirth, match.Players.FirstPlayer.Identity.DateOfBirth);

            // Boundry checks for second player
            Assert.NotNull(match.SecondPlayer);
            Assert.NotNull(match.Players.SecondPlayer);
            Assert.Equal(second.FirstName, match.Players.SecondPlayer.Identity.FirstName);
            Assert.Equal(second.LastName, match.Players.SecondPlayer.Identity.LastName);
            Assert.Equal(second.SurName, match.Players.SecondPlayer.Identity.SurName);
            Assert.Equal(second.DateOfBirth, match.Players.SecondPlayer.Identity.DateOfBirth);

            // Boundry checks for remaining properties of match
            Assert.Equal(5, match.BestOfSets);
            Assert.Null(match.Court);
            Assert.Equal(Status.NoStarted, match.Status);
            Assert.Null(match.StartedOn);
            Assert.Null(match.CompletedOn);
            Assert.Null(match.WonBy);
        }

        /// <summary>
        /// New match with valid players, starting match with multiple times
        /// </summary>
        [Fact]
        public void NewMatchWithValidPlayersScenario2()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();

            // Boundry checks for the players
            Assert.Null(match.FirstPlayer);
            Assert.Null(match.Players.FirstPlayer);
            Assert.Null(match.SecondPlayer);
            Assert.Null(match.Players.SecondPlayer);

            // Initilalize match players
            match.Players.FirstPlayer = new Player(first);
            match.Players.SecondPlayer = new Player(second);
            match.NewMatch("First Match");

            // Boundry checks for first player
            Assert.NotNull(match.FirstPlayer);
            Assert.NotNull(match.Players.FirstPlayer);
            Assert.Equal(first.FirstName, match.Players.FirstPlayer.Identity.FirstName);
            Assert.Equal(first.LastName, match.Players.FirstPlayer.Identity.LastName);
            Assert.Equal(first.SurName, match.Players.FirstPlayer.Identity.SurName);
            Assert.Equal(first.DateOfBirth, match.Players.FirstPlayer.Identity.DateOfBirth);

            // Boundry checks for second player
            Assert.NotNull(match.SecondPlayer);
            Assert.NotNull(match.Players.SecondPlayer);
            Assert.Equal(second.FirstName, match.Players.SecondPlayer.Identity.FirstName);
            Assert.Equal(second.LastName, match.Players.SecondPlayer.Identity.LastName);
            Assert.Equal(second.SurName, match.Players.SecondPlayer.Identity.SurName);
            Assert.Equal(second.DateOfBirth, match.Players.SecondPlayer.Identity.DateOfBirth);

            // Boundry checks for remaining properties of match
            Assert.Equal(5, match.BestOfSets);
            Assert.Null(match.Court);
            Assert.Equal(Status.NoStarted, match.Status);
            Assert.Null(match.StartedOn);
            Assert.Null(match.CompletedOn);
            Assert.Null(match.WonBy);

            // Start Second match
            // Initilalize match players, for second match
            match.Players.RemoveAll();                                      // Before starging remove the players
            match.Players.FirstPlayer = new Player(second);
            match.Players.SecondPlayer = new Player(first);
            match.NewMatch("Second Match");

            // Boundry checks for first player
            Assert.NotNull(match.FirstPlayer);
            Assert.NotNull(match.Players.FirstPlayer);
            Assert.Equal(second.FirstName, match.Players.FirstPlayer.Identity.FirstName);
            Assert.Equal(second.LastName, match.Players.FirstPlayer.Identity.LastName);
            Assert.Equal(second.SurName, match.Players.FirstPlayer.Identity.SurName);
            Assert.Equal(second.DateOfBirth, match.Players.FirstPlayer.Identity.DateOfBirth);

            // Boundry checks for second player
            Assert.NotNull(match.SecondPlayer);
            Assert.NotNull(match.Players.SecondPlayer);
            Assert.Equal(first.FirstName, match.Players.SecondPlayer.Identity.FirstName);
            Assert.Equal(first.LastName, match.Players.SecondPlayer.Identity.LastName);
            Assert.Equal(first.SurName, match.Players.SecondPlayer.Identity.SurName);
            Assert.Equal(first.DateOfBirth, match.Players.SecondPlayer.Identity.DateOfBirth);

            // Boundry checks for remaining properties of match
            Assert.Equal(5, match.BestOfSets);
            Assert.Null(match.Court);
            Assert.Equal(Status.NoStarted, match.Status);
            Assert.Null(match.StartedOn);
            Assert.Null(match.CompletedOn);
            Assert.Null(match.WonBy);
        }

        /// <summary>
        /// New match with valid players by passing 2 parameters
        /// </summary>
        [Fact]
        public void NewMatchWithValidPlayersScenario3()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();

            // Boundry checks for the players
            Assert.Null(match.FirstPlayer);
            Assert.Null(match.Players.FirstPlayer);
            Assert.Null(match.SecondPlayer);
            Assert.Null(match.Players.SecondPlayer);

            // Initilalize match players
            match.Players.FirstPlayer = new Player(first);
            match.Players.SecondPlayer = new Player(second);
            match.NewMatch("First Match", 3);

            // Boundry checks for first player
            Assert.NotNull(match.FirstPlayer);
            Assert.NotNull(match.Players.FirstPlayer);
            Assert.Equal(first.FirstName, match.Players.FirstPlayer.Identity.FirstName);
            Assert.Equal(first.LastName, match.Players.FirstPlayer.Identity.LastName);
            Assert.Equal(first.SurName, match.Players.FirstPlayer.Identity.SurName);
            Assert.Equal(first.DateOfBirth, match.Players.FirstPlayer.Identity.DateOfBirth);

            // Boundry checks for second player
            Assert.NotNull(match.SecondPlayer);
            Assert.NotNull(match.Players.SecondPlayer);
            Assert.Equal(second.FirstName, match.Players.SecondPlayer.Identity.FirstName);
            Assert.Equal(second.LastName, match.Players.SecondPlayer.Identity.LastName);
            Assert.Equal(second.SurName, match.Players.SecondPlayer.Identity.SurName);
            Assert.Equal(second.DateOfBirth, match.Players.SecondPlayer.Identity.DateOfBirth);

            // Boundry checks for remaining properties of match
            Assert.NotNull(match.Name);
            Assert.Equal(3, match.BestOfSets);
            Assert.Null(match.Court);
            Assert.Equal(Status.NoStarted, match.Status);
            Assert.Null(match.StartedOn);
            Assert.Null(match.CompletedOn);
            Assert.Null(match.WonBy);
        }

        /// <summary>
        /// New match with valid players by passing 3 parameters
        /// </summary>
        [Fact]
        public void NewMatchWithValidPlayersScenario4()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();

            // Boundry checks for the players
            Assert.Null(match.FirstPlayer);
            Assert.Null(match.Players.FirstPlayer);
            Assert.Null(match.SecondPlayer);
            Assert.Null(match.Players.SecondPlayer);

            // Initilalize match players
            match.Players.FirstPlayer = new Player(first);
            match.Players.SecondPlayer = new Player(second);
            match.NewMatch("First Match", "First Match Court", 5);

            // Boundry checks for first player
            Assert.NotNull(match.FirstPlayer);
            Assert.NotNull(match.Players.FirstPlayer);
            Assert.Equal(first.FirstName, match.Players.FirstPlayer.Identity.FirstName);
            Assert.Equal(first.LastName, match.Players.FirstPlayer.Identity.LastName);
            Assert.Equal(first.SurName, match.Players.FirstPlayer.Identity.SurName);
            Assert.Equal(first.DateOfBirth, match.Players.FirstPlayer.Identity.DateOfBirth);

            // Boundry checks for second player
            Assert.NotNull(match.SecondPlayer);
            Assert.NotNull(match.Players.SecondPlayer);
            Assert.Equal(second.FirstName, match.Players.SecondPlayer.Identity.FirstName);
            Assert.Equal(second.LastName, match.Players.SecondPlayer.Identity.LastName);
            Assert.Equal(second.SurName, match.Players.SecondPlayer.Identity.SurName);
            Assert.Equal(second.DateOfBirth, match.Players.SecondPlayer.Identity.DateOfBirth);

            // Boundry checks for remaining properties of match
            Assert.NotNull(match.Name);
            Assert.Equal(5, match.BestOfSets);
            Assert.NotNull(match.Court);
            Assert.Equal(Status.NoStarted, match.Status);
            Assert.Null(match.StartedOn);
            Assert.Null(match.CompletedOn);
            Assert.Null(match.WonBy);
        }

        /// <summary>
        /// Starting a new match invalid players who is not present the palyer database
        /// </summary>
        [Fact]
        public void NewMatchWithPlayersNotPresentPlayersDatabase()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };
            var third = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Syed",
                SurName = "Ghouse",
                LastName = "Ghouse",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Viernheim Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();

            // Add invlaid player for First player
            match.Players.RemoveAll();                                      // Before starging remove the players
            match.Players.FirstPlayer = new Player(first);
            match.Players.SecondPlayer = new Player(third);

            // Exception expected when start a match with invalid player
            Exception exception = Assert.Throws<MatchFrameworkException>(
                () => match.NewMatch("First Match"));
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Starting a new match without players
        /// </summary>
        [Fact]
        public void NewMatchWithoutPlayers()
        {
            // Prepare player details
            var first = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                SurName = "Doe",
                LastName = "Joe",
                DateOfBirth = new DateTime(1976, 12, 4),
                Club = "Estern Club"
            };
            var second = new PlayerEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bernhard",
                SurName = "Ritter",
                LastName = "Abraham",
                DateOfBirth = new DateTime(1971, 6, 2),
                Club = "Hungarian Club"
            };

            // Prepare mock for persistence GetPlayers method
            var players = new List<PlayerEntity>() { first, second };
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Returns(players);

            // Initialize new match
            IMatch match = GetMatch();

            // Boundry checks for the players
            Assert.Null(match.FirstPlayer);
            Assert.Null(match.Players.FirstPlayer);
            Assert.Null(match.SecondPlayer);
            Assert.Null(match.Players.SecondPlayer);

            // Exception expected when start a match without players
            Exception exception = Assert.Throws<MatchFrameworkException>(() => match.NewMatch("First Match"));
            Assert.NotNull(exception);
        }
    }
}
