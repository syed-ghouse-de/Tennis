﻿using Hexagon.Game.Tennis.Entity;
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
        Players Players { get; set; }                       // Player list        
        ScoreEntity Score { get; }                          // To maintain the player score
        MatchEntity Info { get; set; }
        
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
    }
}
