using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Domain.Implemenation;
using Hexagon.Game.Tennis.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Hexagon.Game.Tennis.Test.Domain
{
    /// <summary>
    /// Coverage of test cases for player domain service
    /// </summary>
    public class PlayerDomainServiceTest
    {
        // Member variable objects for persistence mock 
        private Mock<IPlayerPersistenceService> _playerPersistenceMock;

        // Member variable objects for domain serivce
        private IPlayerDomainService _playerDomainService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerDomainServiceTest()
        {    
            _playerPersistenceMock = new Mock<IPlayerPersistenceService>();
            _playerDomainService = new PlayerDomainService(_playerPersistenceMock.Object);
        }

        /// <summary>
        /// Get all available players from persistence service
        /// </summary>
        [Fact]
        public void GetAllPlayersWhichReturnListOfTwoPlayers()
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

            // Check for boundry conditions
            var actual = _playerDomainService.GetPlayers();
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
            Assert.True(Equals(first, actual[0]));
            Assert.True(Equals(second, actual[1]));
        }

        /// <summary>
        /// Get all available players which throws persistence exception
        /// </summary>
        [Fact]
        public void GetAllPlayersWhichThrowsPersistenceException()
        {            
            // Expected exception is DomainServiceException when 
            // persistence layer thows PersistenceServiceException 
            _playerPersistenceMock.Setup(s => s.GetPlayers()).Throws(new PersistenceServiceException());        
            Assert.Throws<DomainServiceException>(() => _playerDomainService.GetPlayers());
        }
    }
}
