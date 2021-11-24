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
    /// Coverage of test cases for match domain service
    /// </summary>
    public class MatchDomainServiceTest
    {
        // Member variable objects for persistence mock 
        private Mock<IMatchPersistenceService> _matchPersistenceMock;

        // Member variable objects for domain serivce
        private IMatchDomainService _matchDomainService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchDomainServiceTest()
        {
            _matchPersistenceMock = new Mock<IMatchPersistenceService>();
            _matchDomainService = new MatchDomainService(_matchPersistenceMock.Object);
        }

        /// <summary>
        /// Adding match details using persistence
        /// </summary>
        [Fact]
        public void AddMatchDetails()
        {
            // Prepare match information
          var match = new MatchEntity()
            {
                Id = Guid.NewGuid(),
                Name = "First Match",
                StartedOn = DateTime.UtcNow,
                CompletedOn = null,
                Status = Status.NoStarted
            };

            // Call method the add match
            _matchDomainService.AddMatch(match);

            // Mock the GetMatch method of match persistence
            _matchPersistenceMock.Setup(m => m.GetMatch(It.IsAny<Guid>())).Returns(match);
            var actual = _matchDomainService.GetMatch(match.Id);

            // Boundry checks for verification
            Assert.NotNull(actual);
            Assert.True(Equals(match, actual));
        }
    }
}
