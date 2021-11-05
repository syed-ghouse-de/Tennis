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
        [Fact]
        public void PlayBestOfThreeSetsWinScenarioOne()
        {
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.NotNull(match.WonBy);
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

        [Fact]
        public void PlayBestOfThreeSetsWinScenarioTwo()
        {
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

            Assert.Null(match.WonBy);
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

            Assert.NotNull(match.WonBy);
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

        [Fact]
        public void PlayBestOfFiveSetsWinScenarioOne()
        {
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.NotNull(match.WonBy);
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

        [Fact]
        public void PlayBestOfFiveSetsWinScenarioTwo()
        {
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.Null(match.WonBy);
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

            Assert.NotNull(match.WonBy);
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
    }
}
