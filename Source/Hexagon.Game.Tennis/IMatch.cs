using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Interface for Match
    /// </summary>
    public interface IMatch
    {
        /// <summary>
        /// ScoreUpdate action event for monitoring score
        /// </summary>
        event Action<PlayerEntity, ScoreEntity> ScoreUpdate;        // Delegate for player point win    
        
        /// <summary>
        /// MatchWin action event for to notify winner of the match
        /// </summary>
        event Action<PlayerEntity, ScoreEntity> MatchWin;           // Delegat for player game point win

        /// <summary>
        /// Id of a match
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Name of the match
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Name of the match
        /// </summary>
        string Court { get; set; }

        /// <summary>
        /// Date and time of match when it started
        /// </summary>
        DateTime? StartedOn { get; }

        /// <summary>
        /// Date and time of match when it completed
        /// </summary>
        DateTime? CompletedOn { get; }

        /// <summary>
        /// Status of the match
        /// </summary>
        Status Status { get; }

        /// <summary>
        /// Match best of sets
        /// </summary>
        int BestOfSets { get; set; }

        /// <summary>
        /// Winner of the match
        /// </summary>
        PlayerEntity WonBy { get; }

        /// <summary>
        /// Player List
        /// </summary>
        Players Players { get; set; }                               

        /// <summary>
        /// Score of the match
        /// </summary>
        ScoreEntity Score { get; }

        /// <summary>
        /// First player of the match
        /// </summary>
        IPlayer FirstPlayer { get; set; }

        /// <summary>
        /// Second player of the match
        /// </summary>
        IPlayer SecondPlayer { get; set; }

        /// <summary>
        /// Initialize the new match before starting
        /// </summary>
        /// <param name="match">Match details to start a new match</param>
        void NewMatch(MatchEntity match);

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        void NewMatch(string name);

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        /// <param name="bestOfSets">Match best of sets</param>
        void NewMatch(string name, int bestOfSets);

        /// <summary>
        /// Initialize new match before starting of the match
        /// </summary>
        /// <param name="name">Name of the match</param>
        /// /// <param name="court">Name of the match court</param>
        /// <param name="bestOfSets">Match best of sets</param>
        void NewMatch(string name, string court, int? bestOfSets);

        /// <summary>
        /// To start the match
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the match
        /// </summary>
        void Stop();

        /// <summary>
        /// Get all the players from the database
        /// </summary>
        /// <returns>Return list of players</returns>
        List<PlayerEntity> GetPlayers();
    }
}
