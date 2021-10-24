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
            match.TossWon = firstPlayer;

            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();
       
            Assert.Equal(firstPlayer.Point.Point, PlayerPoint.GamePoint);
            Assert.Equal(secondPlayer.Point.Point, PlayerPoint.Love);

            Assert.Equal(1, match.Score.TotalSets);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).Status);

            Assert.Equal(1, match.Score.GetSet(0).TotalGames);
            Assert.Equal(Status.InProgress, match.Score.GetSet(0).GetGame(0).Status); 
            
            Assert.Equal(Status.InProgress, match.Score.CurrentSet.Status);
            Assert.Equal(Status.InProgress, match.Score.CurrentGame.Status);

            Assert.Equal(match.TossWon.Identity.FirstName, 
                match.Score.GetSet(0).GetGame(0).Server.FirstName);

            Assert.Equal("Fifteen-Thirty-Forty-GamePoint", string.Join("-",
                match.Score.CurrentGame.PlayerPoints.Where(
                    p => p.Server.Id.Equals(firstPlayer.Identity.Id)).Select(s => s.Server.Point)));
            Assert.Equal("Love-Love-Love-Love", string.Join("-",
                match.Score.CurrentGame.PlayerPoints.Where(
                    p => p.Receiver.Id.Equals(firstPlayer.Opponent.Identity.Id)).Select(s => s.Receiver.Point)));
        }
    }
}
