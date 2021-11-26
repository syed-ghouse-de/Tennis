using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
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
        // Member variable objects for persistence mock
        private Mock<IMatchPersistenceService> _matchPersistenceMock;
        private Mock<IPlayerPersistenceService> _playerPersistenceMock;
        private Mock<IScorePersistenceService> _scorePersistenceMock;

        private MatchHandler _matchHandler;
        private IMatch _match;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MatchHandlerTest()
        {
            _matchPersistenceMock = new  Mock<IMatchPersistenceService>();
            _playerPersistenceMock = new Mock<IPlayerPersistenceService>();
            _scorePersistenceMock = new Mock<IScorePersistenceService>();

            // Create an instance of Match object by passing Mock objects
            _match = new Match(_matchPersistenceMock.Object,
                _playerPersistenceMock.Object, _scorePersistenceMock.Object);    
            _matchHandler = new MatchHandler((Match)_match);
        }

        /// <summary>
        /// Get the instance of Match object
        /// </summary>
        /// <returns>Return of type IMatch</returns>
        private IMatch GetMatch()
        {
            // Return the instance of Match object
            return _match;
        }

        /// <summary>
        /// Score board points with first set and first game
        /// </summary>
        [Fact]
        public void ScoreBoardPointWinWithFirstSetAndFirstGame()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

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

            // Player current point boundry check
            Assert.Equal("0", model.Players[0].Sets[0]);
            Assert.Equal("0", model.Players[1].Sets[0]);
            Assert.Equal("40", model.Players[0].Point);                     // Forty
            Assert.Equal("0", model.Players[1].Point);                      // Love

            // Player referee score board
            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0", receiver.Point);

            // Second player win
            secondPlayer.Win();
            model = _matchHandler.GetScore(match.Score);
            Assert.Equal("40", model.Players[0].Point);                         // Forty
            Assert.Equal("15", model.Players[1].Point);                         // Fifteen

            // Players referee score board
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();           
            Assert.Equal("R: 0 0 0 15", receiver.Point);

            // First player win
            firstPlayer.Win();
            model = _matchHandler.GetScore(match.Score);
            Assert.Equal("G", model.Players[0].Point);                          // GamePoin
            Assert.Equal("15", model.Players[1].Point);                         // Fifteen

            // Total games won by players
            Assert.Equal("1", model.Players[0].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            Assert.Equal("0", model.Players[1].GamesWon.Where(
                g => g.Key.Equals(match.Score.Sets[0].Id)).Select(s => s.Value).FirstOrDefault().ToString());

            // Player referee score board
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40 G", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0 15 15", receiver.Point);
        }

        /// <summary>
        /// Score board for swapping sever and receiver
        /// </summary>
        [Fact]
        public void ScoreBoardPointWinWithSwapingOfSeverAndReceiver()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

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

            // Score board of the players
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

            // Players referee score board
            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(firstPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15 30 40 40 G", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0 0 0 15 15", receiver.Point);
           
            // After win, players referee score board
            model = _matchHandler.GetScore(match.Score);
            server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[1].Server).FirstOrDefault();
            Assert.Equal(secondPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 15", server.Point);
            receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[1].Receiver).FirstOrDefault();            
            Assert.Equal("R: 0", receiver.Point);
        }

        /// <summary>
        /// Score board with multiple Decue & Advantages
        /// </summary>
        [Fact]
        public void ScoreBoardPointWinWithWithMultipleDeuceAndAdvantages()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);

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

            // Score board for the players
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

            // Score boad of the players
            var server = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Server).FirstOrDefault();
            Assert.Equal(secondPlayer.Identity.FirstName, server.FirstName);
            Assert.Equal("S: 0 15 15 15 30 40 40 40 A 40 40 40 A G", server.Point);
            var receiver = model.Games.Where(k => k.Key.Equals(match.Score.Sets[0].Id))
                .Select(s => s.Value[0].Receiver).FirstOrDefault();           
            Assert.Equal("R: 15 15 30 40 40 40 A 40 40 40 A 40 40 40", receiver.Point);
        }

        /// <summary>
        /// Score board for best of three wins
        /// </summary>
        [Fact]
        public void ScoreBoardForBestOfThreeSetsWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);
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
                Assert.Equal((gameCount + 1).ToString(), model.Players[0].Sets[2]);
                Assert.Equal("0", model.Players[1].Sets[2]);

                Assert.Equal((gameCount + 1).ToString(), model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal("0", model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[2].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            // Score board of the players
            var finalModel = _matchHandler.GetScore(match.Score);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[0].Sets.Count);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[1].Sets.Count);

            Assert.Equal("6", finalModel.Players[0].Sets[0].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[0].ToString());

            Assert.Equal("0", finalModel.Players[0].Sets[1].ToString());
            Assert.Equal("6", finalModel.Players[1].Sets[1].ToString());

            Assert.Equal("6", finalModel.Players[0].Sets[2].ToString()); 
            Assert.Equal("0", finalModel.Players[1].Sets[2].ToString());

            // Winner of the match palyers details
            Assert.NotNull(finalModel.Match.WonBy);
            Assert.Equal(finalModel.Players[0].FirstName, finalModel.Match.WonBy.FirstName);
            Assert.Equal(finalModel.Players[0].SurName, finalModel.Match.WonBy.SurName);
            Assert.Equal(finalModel.Players[0].LastName, finalModel.Match.WonBy.LastName);
        }

        /// <summary>
        /// Score board for the Best of five win
        /// </summary>
        [Fact]
        public void ScoreBoardForBestOfFiveSetsWin()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);
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
                Assert.Equal("0", model.Players[0].Sets[4]);
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].Sets[4]);

                Assert.Equal("0", model.Players[0].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[4].Id)).Select(s => s.Value).FirstOrDefault().ToString());
                Assert.Equal((gameCount + 1).ToString(), model.Players[1].GamesWon.Where(
                    g => g.Key.Equals(match.Score.Sets[4].Id)).Select(s => s.Value).FirstOrDefault().ToString());
            }

            // Score board of the players
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

            // Winner of the match player details
            Assert.NotNull(finalModel.Match.WonBy);
            Assert.Equal(finalModel.Players[1].FirstName, finalModel.Match.WonBy.FirstName);
            Assert.Equal(finalModel.Players[1].SurName, finalModel.Match.WonBy.SurName);
            Assert.Equal(finalModel.Players[1].LastName, finalModel.Match.WonBy.LastName);
        }

        /// <summary>
        /// Player continue to aplyer after winning the match
        /// </summary>
        [Fact]
        public void ThrowExceptionWhenPlayerContinueToPlayAfterWinningTheMatch()
        {
            // Match and players details
            IMatch match = GetMatch();
            AddPlayers(match);
            match.BestOfSets = 5;   

            var firstPlayer = match.Players.FirstPlayer;
            var secondPlayer = match.Players.SecondPlayer;
            match.Players.Server = firstPlayer;
            match.Start();

            // First player set win
            for (int gameCount = 0; gameCount < (3 * 6 * 4); gameCount++)
                firstPlayer.Win();

            // Score board of the players
            var finalModel = _matchHandler.GetScore(match.Score);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[0].Sets.Count);
            Assert.Equal(_matchHandler.Match.BestOfSets, finalModel.Players[1].Sets.Count);

            Assert.Equal("6", finalModel.Players[0].Sets[0].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[0].ToString());

            Assert.Equal("6", finalModel.Players[0].Sets[1].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[1].ToString());

            Assert.Equal("6", finalModel.Players[0].Sets[2].ToString());
            Assert.Equal("0", finalModel.Players[1].Sets[2].ToString());

            Assert.Equal(String.Empty, finalModel.Players[0].Sets[3].ToString());
            Assert.Equal(String.Empty, finalModel.Players[1].Sets[3].ToString());

            Assert.Equal(String.Empty, finalModel.Players[0].Sets[4].ToString());
            Assert.Equal(String.Empty, finalModel.Players[1].Sets[4].ToString());

            // Winner of the match players detail
            Assert.NotNull(finalModel.Match.WonBy);
            Assert.Equal(finalModel.Players[0].FirstName, finalModel.Match.WonBy.FirstName);
            Assert.Equal(finalModel.Players[0].SurName, finalModel.Match.WonBy.SurName);
            Assert.Equal(finalModel.Players[0].LastName, finalModel.Match.WonBy.LastName);

            // Throw exception when palyer try to play after match is completed
            Exception exception = Assert.Throws<AlreadyMatchWonException>(() => firstPlayer.Win());
            Assert.NotNull(exception);
        }
    }
}
