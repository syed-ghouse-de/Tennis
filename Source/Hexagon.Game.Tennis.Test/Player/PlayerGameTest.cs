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
    /// Player Game unit test cases
    /// </summary>
    public class PlayerGameTest : BaseTest
    {
        [Fact]
        public void PlayGameWithFirstInprogressGame()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).Status);

            Assert.Equal(1, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(0).Status);

            Assert.Equal(match.Players.Server.Identity.FirstName,
                match.Score.GetSet(0).GetGame(0).Server.FirstName);
            Assert.Equal(match.Players.FirstPlayer.Identity.FirstName,
                match.Score.GetSet(0).GetGame(0).Server.FirstName);

            firstPlayer.Win();

            Assert.Equal(firstPlayer.Point.Point, PlayerPoint.GamePoint);
            Assert.Equal(secondPlayer.Point.Point, PlayerPoint.Love);

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).Status);

            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status); 

            Assert.Equal(Status.InProgress, match.Score.CurrentSet.Status);
            Assert.Equal(Status.InProgress, match.Score.CurrentGame.Status);

            Assert.Equal(match.Players.Server.Identity.FirstName,
                match.Score.GetSet(0).GetGame(1).Server.FirstName);
            Assert.Equal(match.Players.SecondPlayer.Identity.FirstName,
                match.Score.GetSet(0).GetGame(1).Server.FirstName);

            var points = string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString()));

            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithPlayerPoints()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            secondPlayer.Win();         // 15   15
            firstPlayer.Win();          // 30   15
            firstPlayer.Win();          // 40   15
            secondPlayer.Win();         // 40   30  

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithFirstPlayerGamePoint()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Thirty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithFirstPlayerStraightGamePointWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();          // 15   0
            firstPlayer.Win();          // 30   0
            firstPlayer.Win();          // 40   0    
            firstPlayer.Win();          // Game 0

            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithSecondPlayerStraightGamePointWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            secondPlayer.Win();          // 0   15
            secondPlayer.Win();          // 0   30
            secondPlayer.Win();          // 0   40    
            secondPlayer.Win();          // 0   Game

            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        [Fact]
        public void PlayGameWithDeuce()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithSecondPlayerAdvantage()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Advantage", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithFirstPlayerAdvantage()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantages()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));
        }

        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantagesFirstPlayerWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty-Advantage-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        [Fact]
        public void PlayGameWithMultipleDeuceAndAdvantagesSecondPlayerWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

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

            Assert.Equal("Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-Forty-Forty-Forty", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Point.ToString())));
            Assert.Equal("Love-Fifteen-Fifteen-Fifteen-Thirty-Forty-Forty-Forty-Advantage-Forty-Forty-Forty-Advantage-GamePoint", string.Join("-",
                 match.Score.GetSet(0).GetGame(0).PlayerPoints.Where(
                    p => p.Player.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Point.ToString())));

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(2, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.Completed, match.Score.GetSet(0).GetGame(0).Status);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(1).Status);
        }

        [Fact]
        public void GetCurrentSetDetailsWithoutStartingOfMatch()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;            

            Exception exception = Assert.Throws<NotStartedException>(() => match.Score.CurrentSet);
            Assert.NotNull(exception);
        }

        [Fact]
        public void GetCurrentGameDetailsWithoutStartingOfMatch()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;

            Exception exception = Assert.Throws<NotStartedException>(() => match.Score.CurrentGame);
            Assert.NotNull(exception);
        }

        [Fact]
        public void GetSetDetailsWithoutStartingOfMatch()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;

            Exception exception = Assert.Throws<NotStartedException>(() => match.Score.GetSet(0));
            Assert.NotNull(exception);
        }
    }
}


