using Hexagon.Game.Framework.Exceptions;
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
    /// Unit test class to cover Set & Match points
    /// </summary>
    public class PlayerSetAndMatchPointTest : BaseTest
    {
        /// <summary>
        /// Best of 3 Sets win scenario 1
        /// </summary>
        [Fact]
        public void PlayBestOfThreeSetsWinScenarioOne()
        {
            // Match and players details
            IMatch match = new Match();
            match.Players = AddPlayers();    
            match.BestOfSets = 3;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(2, match.Score.TotalSets);
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Second player Set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();        // 15            0
                firstPlayer.Win();         // 15           15
                secondPlayer.Win();        // 30           15
                secondPlayer.Win();        // 40           15
                firstPlayer.Win();         // 40           30
                firstPlayer.Win();         // 40           40      Deuce            
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40      Deuce
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // 40           40      Deuce
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // Advantage    40
                secondPlayer.Win();        // GamePoint    40
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(3, match.Score.TotalSets);
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.NotNull(match.WonBy);
            Assert.Equal(Status.Completed, match.Status);
            Assert.Equal(firstPlayer.Identity.Id, match.WonBy.Id);
            Assert.Equal(3, match.Score.TotalSets);
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());
        }

        /// <summary>
        /// Best of 3 Sets wins Scanario 2
        /// </summary>
        [Fact]
        public void PlayBestOfThreeSetsWinScenarioTwo()
        {
            // Match & players details
            IMatch match = new Match();
            match.Players = AddPlayers();            ;
            match.BestOfSets = 3;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(2, match.Score.TotalSets);
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.NotNull(match.WonBy);
            Assert.Equal(Status.Completed, match.Status);
            Assert.Equal(firstPlayer.Identity.Id, match.WonBy.Id);
            Assert.Equal(2, match.Score.TotalSets);
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());
        }

        /// <summary>
        /// Best of 5 Sets win Scenario 1
        /// </summary>
        [Fact]
        public void PlayBestOfFiveSetsWinScenarioOne()
        {
            // Match and players details
            IMatch match = new Match();
            match.Players = AddPlayers();            
            match.BestOfSets = 5;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(2, match.Score.TotalSets);
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Second player Set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();        // 15            0
                firstPlayer.Win();         // 15           15
                secondPlayer.Win();        // 30           15
                secondPlayer.Win();        // 40           15
                firstPlayer.Win();         // 40           30
                firstPlayer.Win();         // 40           40      Deuce            
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40      Deuce
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // 40           40      Deuce
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // Advantage    40
                secondPlayer.Win();        // GamePoint    40
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(3, match.Score.TotalSets);
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(4, match.Score.TotalSets);
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.NotNull(match.WonBy);
            Assert.Equal(Status.Completed, match.Status);
            Assert.Equal(firstPlayer.Identity.Id, match.WonBy.Id);
            Assert.Equal(4, match.Score.TotalSets);
            Assert.Equal(4, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());
        }

        /// <summary>
        /// Best of 5 Sets win Scenario 2
        /// </summary>
        [Fact]
        public void PlayBestOfFiveSetsWinScenarioTwo()
        {
            // Match and players details
            IMatch match = new Match();
            match.Players = AddPlayers();          
            match.BestOfSets = 5;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(2, match.Score.TotalSets);
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Second player Set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();        // 15            0
                firstPlayer.Win();         // 15           15
                secondPlayer.Win();        // 30           15
                secondPlayer.Win();        // 40           15
                firstPlayer.Win();         // 40           30
                firstPlayer.Win();         // 40           40      Deuce            
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40      Deuce
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // 40           40      Deuce
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // Advantage    40
                secondPlayer.Win();        // GamePoint    40
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(3, match.Score.TotalSets);
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
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
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(4, match.Score.TotalSets);
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Second player Set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();        // 15            0
                firstPlayer.Win();         // 15           15
                secondPlayer.Win();        // 30           15
                secondPlayer.Win();        // 40           15
                firstPlayer.Win();         // 40           30
                firstPlayer.Win();         // 40           40      Deuce            
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40      Deuce
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // 40           40      Deuce
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // Advantage    40
                secondPlayer.Win();        // GamePoint    40
            }

            // Match & Set boundry checks
            Assert.Null(match.WonBy);
            Assert.Equal(Status.InProgress, match.Status);
            Assert.Equal(5, match.Score.TotalSets);
            Assert.Equal(4, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(1, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Second player Set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();        // 15            0
                firstPlayer.Win();         // 15           15
                secondPlayer.Win();        // 30           15
                secondPlayer.Win();        // 40           15
                firstPlayer.Win();         // 40           30
                firstPlayer.Win();         // 40           40      Deuce            
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40      Deuce
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // 40           40      Deuce
                secondPlayer.Win();        // Advantage    40
                firstPlayer.Win();         // 40           40
                secondPlayer.Win();        // Advantage    40
                secondPlayer.Win();        // GamePoint    40
            }

            // Match & Set boundry checks
            Assert.NotNull(match.WonBy);
            Assert.Equal(Status.Completed, match.Status);
            Assert.Equal(secondPlayer.Identity.Id, match.WonBy.Id);
            Assert.Equal(5, match.Score.TotalSets);
            Assert.Equal(5, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(2, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());
        }

        /// <summary>
        /// Exception expected when players play and winning the match
        /// </summary>
        [Fact]
        public void ThrowExceptionWhenPlayerContinueToPlayAfterWinningTheMatch()
        {
            // Match and players details
            IMatch match = new Match();
            match.Players = AddPlayers();
            match.BestOfSets = 5;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < (3 * 6 * 4); gameCount++)
                secondPlayer.Win();

            // Match & Set boundry checks
            Assert.NotNull(match.WonBy);
            Assert.Equal(secondPlayer.Identity.Id, match.WonBy.Id);
            Assert.Equal(3, match.Score.TotalSets);
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.Status.Equals(Status.Completed)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.Status.Equals(Status.InProgress)).Count());
            Assert.Equal(0, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(firstPlayer.Identity.Id)).Count());
            Assert.Equal(3, match.Score.Sets.Where(
                s => s.WonBy != null && s.WonBy.Id.Equals(secondPlayer.Identity.Id)).Count());

            // Exception expected when player trys to play the game afer winning the match
            Exception exception = Assert.Throws<AlreadyMatchWonException>(() => firstPlayer.Win());
            Assert.NotNull(exception);
        }
    }
}
