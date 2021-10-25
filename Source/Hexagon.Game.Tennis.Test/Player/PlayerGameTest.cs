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
        public void PlayerGameWithFirstInprogressGame()
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
    }
}
