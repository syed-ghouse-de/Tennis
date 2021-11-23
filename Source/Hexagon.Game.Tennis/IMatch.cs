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
        event Action<PlayerEntity, ScoreEntity> ScoreUpdate;        // Delegate for player point win       
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
        /// Date and time of match when it started
        /// </summary>
        DateTime StartedOn { get; }

        /// <summary>
        /// Date and time of match when it completed
        /// </summary>
        Nullable<System.DateTime> CompletedOn { get; }

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
        /// Play the match by two players
        /// </summary>
        void Play();

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

        /// <summary>
        /// Initialize the new match before starting
        /// </summary>
        /// <param name="match">Match details to start a new match</param>
        void NewMatch(MatchEntity match);
    }
}
