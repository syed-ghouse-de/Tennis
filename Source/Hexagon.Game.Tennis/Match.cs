using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Tennis.Domain.Service;
using Hexagon.Game.Tennis.Domain.Service.Implemenation;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Match class to contains match associated attributes
    /// </summary>
    public class Match : IMatch
    {
        private ScoreEntity _score;
        private IScoreDomainService _scoreDomainService;                    // Score business logic service     

        public Players Players { get; set; }                                // List of match players
        public ScoreEntity Score { get { return _score; } }                 // To maintain player current point     
        public MatchEntity Info { get; set; }                               // Match information

        /// <summary>
        /// Default constructor
        /// </summary>
        public Match()
        {
            _scoreDomainService = new ScoreDomainService();
            _score = new ScoreEntity();
        }

        /// <summary>
        /// To play the match
        /// </summary>
        public void Play()
        {
            throw new NotImplementedException();            
        }

        /// <summary>
        /// To start the match
        /// </summary>
        public void Start()
        {
            // Get the current score of the match
            _score = _scoreDomainService.GetMatchScore(Info, Players.Server.Identity);

            // 
            this.Players.PointWin += OnPointWin;
            this.Players.GamePointWin += OnGamePointWin;
        }

        /// <summary>
        /// To stop the match
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Player point win
        /// </summary>
        /// <param name="winPlayer">Winner player</param>
        /// <param name="point">Player winner point</param>
        private void OnPointWin(PlayerEntity winPlayer, PlayerPoint point)
        {
            try
            {
                // Calculate score after every point win
                _score = _scoreDomainService.PointWin(Score, Players.Server.Identity, 
                    winPlayer, Players[winPlayer.Id].Opponent.Identity, point);             
            }
            catch (DomainServiceException domainServiceException)
            {                
                throw domainServiceException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    
        /// <summary>
        /// Player game point win
        /// </summary>
        /// <param name="winPlayer">Game point winner</param>
        private void OnGamePointWin(PlayerEntity winPlayer)
        {
            try
            {
                // Calculate game point 
                _score = _scoreDomainService.GamePointWin(
                    Info, Score, Players.Server.Opponent.Identity, winPlayer);

                // Swap the Server afer every game point win
                Players.Server = Players.Server.Opponent;               
            }
            catch (DomainServiceException domainServiceException)
            {
                throw domainServiceException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
