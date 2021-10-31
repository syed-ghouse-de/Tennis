using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Domain.Service;
using Hexagon.Game.Tennis.Domain.Service.Implemenation;
using Hexagon.Game.Tennis.Entity;

using Xunit;

namespace Hexagon.Game.Tennis.Test
{
    /// <summary>
    /// Coverage of test cases for score domain service
    /// </summary>
    public class ScoreDomainServiceTest
    {
        [Fact]
        public void PointWinHavingZeroSets()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

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

            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Love, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            score = scoreService.PointWin(score, server, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            score = scoreService.PointWin(score, server, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
        }

        [Fact]
        public void PointWinWhenLooserIsInAdvantage()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

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

            Assert.Equal(PlayerPoint.Advantage, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Love, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);

            score = scoreService.PointWin(score, looser, looser, winner, PlayerPoint.Fifteen);
            Assert.Equal(PlayerPoint.Forty, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(winner.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
            Assert.Equal(PlayerPoint.Fifteen, score.CurrentGame.PlayerPoints.Where
                (p => p.Player.Id.Equals(looser.Id)).OrderBy(o => o.UpdatedOn).Last().Point);
        }
        
        [Fact]
        public void GamePointWithSimpleWin()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

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

            score = scoreService.GamePointWin(match, score, receiver, winner);
            Assert.Equal(string.Format("{0}, {1}", receiver.FirstName, receiver.SurName),
                string.Format("{0}, {1}", score.CurrentGame.Server.FirstName, score.CurrentGame.Server.SurName));

            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);

            Assert.Equal(2, score.CurrentSet.TotalGames);
            Assert.Equal(Status.Completed, score.CurrentSet.GetGame(0).Status);
            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
        }

        [Fact]
        public void SetWinWithFirstPlayerSixGamePoint()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };            

            Assert.NotNull(scoreService);
            score = scoreService.PointWin(score, firstPlayer, firstPlayer, secondPlayer, PlayerPoint.Forty);
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);

            Assert.Equal(3, score.CurrentSet.TotalGames);
            Assert.Equal(1, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(1, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(3, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(4, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(5, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            
            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(string.Format("{0}, {1}", firstPlayer.FirstName, firstPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));           
            Assert.Equal(6, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
        }

        [Fact]
        public void SetWinWithFirstPlayerFourGamePointAndSecondPlayerSixGamePoint()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

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

        [Fact]
        public void SetWinWithFirstPlayerSevenGamePointAndSecondPlayerFiveGamePoint()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

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

            Assert.Equal(1, score.TotalSets); 
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(6, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(5, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, secondPlayer, firstPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);         
            Assert.Equal(string.Format("{0}, {1}", firstPlayer.FirstName, firstPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(5, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());
        }

        [Fact]
        public void SetWinWithFirstPlayerSevenGamePointAndSecondPlayerNineGamePoint()
        {
            IScoreDomainService scoreService = new ScoreDomainService();

            MatchEntity match = new MatchEntity() { Id = Guid.NewGuid(), Name = "First Match" };
            ScoreEntity score = new ScoreEntity();
            PlayerEntity firstPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Jonh", SurName = "Doe" };
            PlayerEntity secondPlayer = new PlayerEntity()
            { Id = Guid.NewGuid(), FirstName = "Bernhard", SurName = "Ritter" };

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

            Assert.Equal(1, score.TotalSets);
            Assert.Equal(Status.InProgress, score.CurrentSet.Status);
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(8, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            score = scoreService.GamePointWin(match, score, firstPlayer, secondPlayer);
            Assert.Equal(2, score.TotalSets);
            Assert.Equal(Status.Completed, score.GetSet(0).Status);
            Assert.Equal(string.Format("{0}, {1}", secondPlayer.FirstName, secondPlayer.SurName),
                string.Format("{0}, {1}", score.GetSet(0).WonBy.FirstName, score.GetSet(0).WonBy.SurName));
            Assert.Equal(7, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(9, score.GetSet(0).Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());

            Assert.Equal(Status.InProgress, score.CurrentGame.Status);
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(firstPlayer.Id)).Count());
            Assert.Equal(0, score.CurrentSet.Games.Where(
                g => g.WonBy != null && g.WonBy.Id.Equals(secondPlayer.Id)).Count());
        }   
    }
}
