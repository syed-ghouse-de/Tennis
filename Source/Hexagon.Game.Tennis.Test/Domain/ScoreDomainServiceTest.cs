using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Domain.Service;
using Hexagon.Game.Tennis.Domain.Service.Implementation;
using Hexagon.Game.Tennis.Entity;

using Xunit;

namespace Hexagon.Game.Tennis.Test
{
    /// <summary>
    /// Coverage of test cases for score domain service
    /// </summary>
    public class ScoreDomainServiceTest
    {
        /// <summary>
        /// Player points win with Zero Sets
        /// </summary>
        [Fact]
        public void PointWinHavingZeroSets()
        {
            // Score domain service instance
            IScoreDomainService scoreService = new ScoreDomainService();

            // Player details of the match
            ScoreEntity score = new ScoreEntity();
            PlayerEntity winner = new PlayerEntity()
                { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity looser = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };
            PlayerEntity server = looser;

            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, server, winner, looser, PlayerPoint.Fifteen);

            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);

            Assert.Equal(1, score.CurrentSet.TotalGames);
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);

            Assert.Equal(string.Format("{0}, {1}", server.FirstName, server.SurName),                 
                string.Format("{0}, {1}", score.CurrentGame.Server.FirstName, score.CurrentGame.Server.SurName));

            // Score comparision after player win
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Love, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            // Score comparision after player win
            score = scoreService.PointWin(score, server, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            // Score comparision after player win
            score = scoreService.PointWin(score, server, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
        }

        /// <summary>
        /// Score point win when losser is in advantate
        /// </summary>
        [Fact]
        public void PointWinWhenLooserIsInAdvantage()
        {
            // Domain service instance for business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Player details of the match
            ScoreEntity score = new ScoreEntity();
            PlayerEntity winner = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity looser = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };   

            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, looser, winner, looser, PlayerPoint.Advantage);

            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);

            Assert.Equal(1, score.CurrentSet.TotalGames);
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);

            Assert.Equal(string.Format("{0}, {1}", looser.FirstName, looser.SurName),
                string.Format("{0}, {1}", score.CurrentGame.Server.FirstName, score.CurrentGame.Server.SurName));

            // Player is in advantage and their scores
            Assert.Equal(PlayerPoint.Advantage, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Love, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            // The players score after the opponent player when the other player is in Advantage
            score = scoreService.PointWin(score, looser, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Forty, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
        }
        
        /// <summary>
        /// Simple game point win
        /// </summary>
        [Fact]
        public void GamePointWithSimpleWin()
        {
            // Domain service instance for business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Match and player information
            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity winner = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity receiver = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };         

            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, receiver, winner, receiver, PlayerPoint.Advantage);
            
            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);

            Assert.Equal(1, score.CurrentSet.TotalGames);
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);

            // Player game point win when the player is in Advantage
            score = scoreService.GamePointWin(match, score, receiver, winner);
            Assert.Equal(string.Format("{0}, {1}", receiver.FirstName, receiver.SurName),
                string.Format("{0}, {1}", score.CurrentGame.Server.FirstName, score.CurrentGame.Server.SurName));

            // Score boundry check after player game point win
            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);

            Assert.Equal(2, score.CurrentSet.TotalGames);
            Assert.Equal(Status.Completed, score.CurrentSet.GetGame(0).Status);
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
        }

        /// <summary>
        /// First palyer set win after winning the game
        /// </summary>
        [Fact]
        public void SetWinWithFirstPlayerSixGamePoint()
        {
            // Domain service instance for the business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Match and player details 
            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };            

            // Game point win for both the players
            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, firstPlayer, firstPlayer, secondPlayer, PlayerPoint.Forty);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);

            // Current set details after game point win
            Assert.Equal(3, score.CurrentSet.TotalGames);
            Assert.Equal(1, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(1, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            // Current score detail after game point win
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            // Current score detail after game point win
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(3, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            // Current score detail after game point win
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(4, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            // Current score detail after game point win
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(5, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            // Current Set details after winning all the 6 games 
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(string.Format("{0}, {1}", firstPlayer.FirstName, firstPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));           
            Assert.Equal(6, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
        }

        /// <summary>
        /// Set win with 4 game points for Player1 and 6 games point with Player2
        /// </summary>
        [Fact]
        public void SetWinWithFirstPlayerFourGamePointAndSecondPlayerSixGamePoint()
        {
            // Domain service instance for business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Match and players details
            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

            // Score boundries check after Players 10 game point wins
            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, firstPlayer, firstPlayer, secondPlayer, PlayerPoint.Forty);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);            
           
            // Score board boundries check and verification after game point wins
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(string.Format("{0}, {1}", secondPlayer.FirstName, secondPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));
            Assert.Equal(4, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(6, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());
        }

        /// <summary>
        /// Set win for Player1 with 7 game point and Player2 with 5 game point
        /// </summary>
        [Fact]
        public void SetWinWithFirstPlayerSevenGamePointAndSecondPlayerFiveGamePoint()
        {
            // Domain service instance for business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Match and player details
            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

            // Player score boundries check after players gamepoint wins
            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, firstPlayer, firstPlayer, secondPlayer, PlayerPoint.Forty);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);

            // Current score boundries check after player game point wins
            Assert.Equal(1, score.TotalSets); 
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(6, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(5, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            // Score boundery checks for set win
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);         
            Assert.Equal(string.Format("{0}, {1}", firstPlayer.FirstName, firstPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(5, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            // Current set score bound checks after winning the previous Set
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());
        }

        /// <summary>
        /// Set win for Player1 with 7 game point and Player2 with 9 game point
        /// </summary>
        [Fact]
        public void SetWinWithFirstPlayerSevenGamePointAndSecondPlayerNineGamePoint()
        {
            // Domain service instance for business rule execution
            IScoreDomainService scoreService = new ScoreDomainService();

            // Match and player details
            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

            // Player score boundries check after players gamepoint wins
            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, firstPlayer, firstPlayer, secondPlayer, PlayerPoint.Forty);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);

            // Current score boundries check after player game point wins
            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(8, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            // Score boundery checks for set win
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);
            Assert.Equal(string.Format("{0}, {1}", secondPlayer.FirstName, secondPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(9, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            // Current set score bound checks after winning the previous Set
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());
        }   
    }
}
