using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Tennis.Desktop.Handler;
using Hexagon.Game.Tennis.Desktop.Model;
using Hexagon.Game.Tennis.Domain.Service.Implementation;
using Hexagon.Game.Tennis.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Hexagon.Game.Tennis.Test.Desktop
{
    /// <summary>
    /// Class to cover match handler test cases
    /// </summary>
    public class MatchHandlerTest : BaseTest
    {
        private MatchHandler _matchHandler;

        public MatchHandlerTest()
        {
            _matchHandler = (MatchHandler)MatchHandler.Instance;
        }

        [Fact]
        public void ScoreBoardPointWinWithFirstSetAndFirstGame()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();
            _matchHandler.Match = match;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();
            ScoreModel model = _matchHandler.GetScore(match.Score);
            Assert.Equal(2, model.Players.Count);
            Assert.Equal(firstPlayer.Identity.FirstName, model.Players[0].FirstName);
            Assert.Equal(secondPlayer.Identity.FirstName, model.Players[1].FirstName);
            Assert.Equal("0", model.Players[0].Sets[0]);
            Assert.Equal("0", model.Players[1].Sets[0]);
            Assert.Equal("40", model.Players[0].Point);                     // Forty
            Assert.Equal("0", model.Players[1].Point);                      // Love
            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0", receiver.Point);

            secondPlayer.Win();
            model = _matchHandler.GetScore(match.Score);
            Assert.Equal("40", model.Players[0].Point);                         // Forty
            Assert.Equal("15", model.Players[1].Point);                         // Fifteen
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();           
            Assert.Equal("R: 0 0 0 15", receiver.Point);

            firstPlayer.Win();
            model = _matchHandler.GetScore(match.Score);
            Assert.Equal("G", model.Players[0].Point);                          // GamePoin
            Assert.Equal("15", model.Players[1].Point);                         // Fifteen
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40 G", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0 15 15", receiver.Point);
        }

        [Fact]
        public void ScoreBoardPointWinWithSwapingOfSeverAndReceiver()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();
            _matchHandler.Match = match;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            firstPlayer.Win();
            firstPlayer.Win();
            firstPlayer.Win();
            secondPlayer.Win();
            firstPlayer.Win();
            secondPlayer.Win();

            var model = _matchHandler.GetScore(match.Score);
            Assert.Equal(2, model.Players.Count);
            Assert.Equal(firstPlayer.Identity.FirstName, model.Players[0].FirstName);
            Assert.Equal(secondPlayer.Identity.FirstName, model.Players[1].FirstName);
            Assert.Equal("1", model.Players[0].Sets[0]);
            Assert.Equal("0", model.Players[1].Sets[0]);
            Assert.Equal("0", model.Players[0].Point);                     // Love
            Assert.Equal("15", model.Players[1].Point);                    // Fifteen

            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40 G", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0 15 15", receiver.Point);
           
            model = _matchHandler.GetScore(match.Score);
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[1].Server).FirstOrDefault();
            Assert.Equal(secondPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[1].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0", receiver.Point);
        }

        [Fact]
        public void ScoreBoardPointWinWithWithMultipleDeuceAndAdvantages()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();
            _matchHandler.Match = match;

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = secondPlayer;
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

            var model = _matchHandler.GetScore(match.Score);
            Assert.Equal(2, model.Players.Count);
            Assert.Equal(firstPlayer.Identity.FirstName, model.Players[0].FirstName);
            Assert.Equal(secondPlayer.Identity.FirstName, model.Players[1].FirstName);
            Assert.Equal("0", model.Players[0].Sets[0]);
            Assert.Equal("1", model.Players[1].Sets[0]);
            Assert.Equal("40", model.Players[0].Point);                     
            Assert.Equal("G", model.Players[1].Point);                   

            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(secondPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 0 15 15 15 30 40 40 40 A 40 40 40 A G", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();           
            Assert.Equal("R: 15 15 30 40 40 40 A 40 40 40 A 40 40 40", receiver.Point);
        }              
    }
}
