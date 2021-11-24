using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Framework.Exceptions;
using Hexagon.Game.Framework.Service.Domain;
using Hexagon.Game.Framework.Service.Persistence;
using Hexagon.Game.Tennis.Domain.Implemenation;
using Hexagon.Game.Tennis.Domain.Service;
using Hexagon.Game.Tennis.Domain.Service.Implementation;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Match class to contains match associated attributes
    /// </summary>
    public class Match : IMatch
    {
        public event Action<PlayerEntity, ScoreEntity> ScoreUpdate;     // Delegate for player point win       
        public event Action<PlayerEntity, ScoreEntity> MatchWin;        // Delegat for player game point win

        // Privte memeber variables
        private IScoreDomainService _scoreDomainService;                // Score business logic service  
        private IPlayerDomainService _playerDomainService;              // Player business logic service

        private MatchEntity _match;                                     // Match information

        public Players Players { get; set; }                            // List of match players
        public ScoreEntity Score { get { return _match.Score; } }       // To maintain player current point     

        /// <summary>
        /// Id of a match
        /// </summary>
        public Guid Id
        {
            get { return _match.Id; }
        }

        /// <summary>
        /// Name of the match
        /// </summary>
        public string Name
        {
            get { return _match.Name; }
            set { _match.Name = value; }
        }

        /// <summary>
        /// Date and time of match when it started
        /// </summary>
        public DateTime StartedOn
        {
            get { return _match.StartedOn; }
        }

        /// <summary>
        /// Date and time of match when it completed
        /// </summary>
        public Nullable<System.DateTime> CompletedOn
        {
            get { return _match.CompletedOn; }
        }

        /// <summary>
        /// Status of the match
        /// </summary>
        public Status Status
        {
            get { return _match.Status; }
        }

        /// <summary>
        /// Match best of sets
        /// </summary>
        public int BestOfSets
        {
            get { return _match.BestOfSets; }
            set { _match.BestOfSets = value; }
        }

        /// <summary>
        /// Winner of the match
        /// </summary>
        public PlayerEntity WonBy
        {
            get { return _match.WonBy; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Match(IMatchPersistenceService matchPersistenceService,
            IPlayerPersistenceService playerPersistenceService,
            IScorePersistenceService scorePersistenceService)
        {
            _scoreDomainService = new ScoreDomainService(matchPersistenceService, scorePersistenceService);
            _playerDomainService = new PlayerDomainService(playerPersistenceService);

            _match = new MatchEntity();
            Players = new Players();
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
            _match.Score = _scoreDomainService
                .GetMatchScore(_match, Players.Server.Identity);

            _match.Status = Status.InProgress;
            _match.StartedOn = DateTime.UtcNow;

            // Delegate subscribtion
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
        /// Get all the players from the database
        /// </summary>
        /// <returns>Return list of players</returns>
        public List<PlayerEntity> GetPlayers()
        {
            try
            {
                // Get the list of available players 
                return _playerDomainService.GetPlayers();
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
        /// Initialize the new match before starting
        /// </summary>
        /// <param name="match">Match details to start a new match</param>
        public void NewMatch(MatchEntity match)
        {
            _match = new MatchEntity()
            {
                Id = match.Id,
                Name = match.Name,
                BestOfSets = match.BestOfSets
            };

            Players = new Players();
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
                _match.Score = _scoreDomainService.PointWin(Score, Players.Server.Identity, 
                    winPlayer, Players[winPlayer.Id].Opponent.Identity, point);

                // Invoke score update event
                ScoreUpdate?.Invoke(winPlayer, _match.Score);
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
                _match.Score = _scoreDomainService.GamePointWin(
                    _match, Score, Players.Server.Opponent.Identity, winPlayer);

                // Swap the Server afer every game point win                
                Players.Server = Players.Server.Opponent;

                // Invoke score update event
                ScoreUpdate?.Invoke(winPlayer, _match.Score);

                // Invoke match win event
                if (_match.Status.Equals(Status.Completed) && _match.WonBy != null)
                {
                    // Set the Match Ponint to both the players
                    Players.FirstPlayer.SetMatchPoint();
                    Players.SecondPlayer.SetMatchPoint();

                    // Invoke the MathWin delegate to notify
                    MatchWin?.Invoke(_match.WonBy, _match.Score);
                }
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
