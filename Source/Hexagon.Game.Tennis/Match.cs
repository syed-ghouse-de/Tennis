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
        /// <summary>
        /// ScoreUpdate action event for monitoring score
        /// </summary>
        public event Action<PlayerEntity, ScoreEntity> ScoreUpdate;     // Delegate for player point win   

        /// <summary>
        /// MatchWin action event for to notify winner of the match
        /// </summary>
        public event Action<PlayerEntity, ScoreEntity> MatchWin;        // Delegate for player game point win

        /// <summary>
        /// Error action event for to notify Error
        /// </summary>
        public event Action<MessageEntity> Error;                       // Delegate for error message

        // Privte memeber variables
        private IMatchDomainService _matchDomainService;                // Score business logic service  
        private IScoreDomainService _scoreDomainService;                // Score business logic service  
        private IPlayerDomainService _playerDomainService;              // Player business logic service

        private MatchEntity _match;                                     // Match information

        /// <summary>
        /// Players of the match
        /// </summary>
        public Players Players { get; set; }                            // List of match players

        /// <summary>
        /// Score of the match, which contains Set's, Game's and Point's
        /// </summary>
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
        /// Name of the match
        /// </summary>
        public string Court
        {
            get { return _match.Court; }
            set { _match.Court = value; }
        }

        /// <summary>
        /// Date and time of match when it started
        /// </summary>
        public DateTime? StartedOn
        {
            get { return _match.StartedOn; }
        }

        /// <summary>
        /// Date and time of match when it completed
        /// </summary>
        public DateTime? CompletedOn
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
        /// First player of the match
        /// </summary>
        public IPlayer FirstPlayer
        {
            get { return Players.FirstPlayer; }
            set { Players.FirstPlayer = value; }
        }

        /// <summary>
        /// Second player of the match
        /// </summary>
        public IPlayer SecondPlayer
        {
            get { return Players.SecondPlayer; }
            set { Players.SecondPlayer = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Match(IMatchPersistenceService matchPersistenceService,
            IPlayerPersistenceService playerPersistenceService,
            IScorePersistenceService scorePersistenceService)
        {
            // Initialization of domain services
            _matchDomainService = new MatchDomainService(matchPersistenceService);
            _scoreDomainService = new ScoreDomainService(matchPersistenceService, scorePersistenceService);
            _playerDomainService = new PlayerDomainService(playerPersistenceService);

            // Instance of match and players objects
            _match = new MatchEntity();
            Players = new Players();

            // Delegate subscriptions
            this.Players.PointWin += OnPointWin;
            this.Players.GamePointWin += OnGamePointWin;
        }

        /// <summary>
        /// To start the match
        /// </summary>
        public void Start()
        {
            try
            { 
                // Start the new match and get the current score of the match
                _match = _matchDomainService.StartMatch(_match);              
                _match.Score = _scoreDomainService
                    .GetMatchScore(_match, Players.Server.Identity);               
            }
            catch (DomainServiceException domainServiceException)
            {
                throw new MatchFrameworkException(domainServiceException.Message);
            }
            catch (Exception exception)
            {
                throw new MatchFrameworkException(exception.Message);
            } 
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
                throw new MatchFrameworkException(domainServiceException.Message);
            }
            catch (Exception exception)
            {
                throw new MatchFrameworkException(exception.Message);
            }
        }

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        public void NewMatch(string name)
        {
            // Call new match by passing null for court and bestofsets
            NewMatch(name, null, null);
        }

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        /// <param name="bestOfSets">Match best of sets</param>
        public void NewMatch(string name, int bestOfSets)
        {
            // Call new match by passing null for court
            NewMatch(name, null, bestOfSets);
        }

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        /// /// <param name="court">Name of the match court</param>
        /// <param name="bestOfSets">Match best of sets</param>
        public void NewMatch(string name, string court, int? bestOfSets)
        {
            try
            {
                // Validate players before starting a new match
                ValidatePlayers();

                // Initialize match entity with name
                _match = new MatchEntity() { Name = name };
                if (court != null) { _match.Court = court; }
                if (bestOfSets != null) { _match.BestOfSets = (int)bestOfSets; }

                // Add new math in the database
                _matchDomainService.AddMatch(_match);
            }
            catch (DomainServiceException domainServiceException)
            {
                throw new MatchFrameworkException(domainServiceException.Message);
            }
            catch (Exception exception)
            {
                throw new MatchFrameworkException(exception.Message);
            }
        }

        /// <summary>
        /// Validate players before starting a new match
        /// </summary>
        private void ValidatePlayers()
        {
            // Check the assignment of the match player, if not throw an exception
            if (this.FirstPlayer == null || this.SecondPlayer == null)
                throw new MatchFrameworkException("Two players are requried to play match!");

            // Check for the players existence in the database, if not throw an exception
            var players = _playerDomainService.GetPlayers();
            if (!players.Where(p => p.Id.Equals(this.FirstPlayer.Identity.Id)).Any() ||
                !players.Where(p => p.Id.Equals(this.SecondPlayer.Identity.Id)).Any())
            {
                throw new MatchFrameworkException("Invalid player(s), not found in the respository!");
            }
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
                _match.Score = _scoreDomainService.PointWin(_match, Score, 
                    Players.Server.Identity, winPlayer, Players[winPlayer.Id].Opponent.Identity, point);

                // Invoke score update event
                ScoreUpdate?.Invoke(winPlayer, _match.Score);
            }
            catch (DomainServiceException domainServiceException)
            {
                // Invoke error event
                Error?.Invoke(new MessageEntity(domainServiceException.Message));
            }
            catch (Exception exception)
            {
                // Invoke error event
                Error?.Invoke(new MessageEntity(exception.Message));
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
                // Invoke error event
                Error?.Invoke(new MessageEntity(domainServiceException.Message));
            }
            catch (Exception exception)
            {
                // Invoke error event
                Error?.Invoke(new MessageEntity(exception.Message));
            }
        }
    }
}
