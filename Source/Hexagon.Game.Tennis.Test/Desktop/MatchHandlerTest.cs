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

            Assert.Equal("1", model.Players[0].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            Assert.Equal("0", model.Players[1].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());

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
            Assert.Equal("1", model.Players[0].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            Assert.Equal("1", model.Players[0].Sets[0]);
            Assert.Equal("0", model.Players[1].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
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
            Assert.Equal("0", model.Players[0].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            Assert.Equal("1", model.Players[1].Sets[0]);
            Assert.Equal("1", model.Players[1].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
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

        [Fact]
        public void ScoreBoardForBestOfThreeSetsWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers(); ;
            match.BestOfSets = 3;
            _matchHandler.Match = match;

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

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[0].Sets[0]);
                Assert.Equal("0", model.Players[1].Sets[0]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

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

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].Sets[1]);
                Assert.Equal("0", model.Players[0].Sets[1]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[1].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[1].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                firstPlayer.Win();          // 15            0               
                firstPlayer.Win();          // 30            0
                firstPlayer.Win();          // 40            0
                firstPlayer.Win();          // GamePoint     0

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[0].Sets[2]);
                Assert.Equal("0", model.Players[1].Sets[2]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            var finalModel = _matchHandler.GetScore(match.Score);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[0].Sets.Count);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[1].Sets.Count);

            Assert.Equal("6", finalModel.Players[0].Sets[0].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[0].ToString());

            Assert.Equal("0", finalModel.Players[0].Sets[1].ToString());
            Assert.Equal("6", finalModel.Players[1].Sets[1].ToString());

            Assert.Equal("6", finalModel.Players[0].Sets[2].ToString()); 
            Assert.Equal("0", finalModel.Players[1].Sets[2].ToString());

            Assert.NotNull(finalModel.Match.WonBy);
            Assert.Equal(finalModel.Players[0].FirstName, finalModel.Match.WonBy.FirstName);
            Assert.Equal(finalModel.Players[0].SurName, finalModel.Match.WonBy.SurName);
            Assert.Equal(finalModel.Players[0].LastName, finalModel.Match.WonBy.LastName);
        }

        [Fact]
        public void ScoreBoardForBestOfFiveSetsWin()
        {
            IMatch match = new Match();
            match.Players = AddPlayers();
            match.BestOfSets = 5;
            _matchHandler.Match = match;

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

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[0].Sets[0]);
                Assert.Equal("0", model.Players[1].Sets[0]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

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

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].Sets[1]);
                Assert.Equal("0", model.Players[0].Sets[1]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[1].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[1].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            // First player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                firstPlayer.Win();          // 15            0               
                firstPlayer.Win();          // 30            0
                firstPlayer.Win();          // 40            0
                firstPlayer.Win();          // GamePoint     0

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal((gameCount + 1).ToString(), model.Players[0].Sets[2]);
                Assert.Equal("0", model.Players[1].Sets[2]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

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

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal("0", model.Players[0].Sets[3]);
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].Sets[3]);                

                Assert.Equal("0", model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[3].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[3].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            // Second player set win
            for (int gameCount = 0; gameCount < 6; gameCount++)
            {
                secondPlayer.Win();          // 15            0               
                secondPlayer.Win();          // 30            0
                secondPlayer.Win();          // 40            0
                secondPlayer.Win();          // GamePoint     0

                var model = _matchHandler.GetScore(match.Score);
                Assert.Null(model.Match.WonBy);
                Assert.Equal("0", model.Players[0].Sets[4]);
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].Sets[4]);

                Assert.Equal("0", model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[4].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[4].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            var finalModel = _matchHandler.GetScore(match.Score);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[0].Sets.Count);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[1].Sets.Count);

            Assert.Equal("6", finalModel.Players[0].Sets[0].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[0].ToString());

            Assert.Equal("0", finalModel.Players[0].Sets[1].ToString());
            Assert.Equal("6", finalModel.Players[1].Sets[1].ToString());

            Assert.Equal("6", finalModel.Players[0].Sets[2].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[2].ToString());

            Assert.Equal("0", finalModel.Players[0].Sets[3].ToString());
            Assert.Equal("6", finalModel.Players[1].Sets[3].ToString());

            Assert.Equal("0", finalModel.Players[0].Sets[4].ToString());
            Assert.Equal("6", finalModel.Players[1].Sets[4].ToString());

            Assert.NotNull(finalModel.Match.WonBy);
            Assert.Equal(finalModel.Players[1].FirstName, finalModel.Match.WonBy.FirstName);
            Assert.Equal(finalModel.Players[1].SurName, finalModel.Match.WonBy.SurName);
            Assert.Equal(finalModel.Players[1].LastName, finalModel.Match.WonBy.LastName);
        }
    }
}
