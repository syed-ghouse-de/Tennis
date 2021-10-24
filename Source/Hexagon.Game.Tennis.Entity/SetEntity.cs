﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    /// <summary>
    /// Class to manage Set
    /// </summary>
    public class SetEntity : BaseEntity
    {
        public List<GameEntity> Games { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SetEntity() { }
     
        /// <summary>
        /// Get the total number of Games
        /// </summary>
        public int TotalGames { get { return Games.Count; } }

        /// <summary>
        /// Get the Game details for the specicific set number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public GameEntity GetGame(int number)
        {
            return Games[number];
        }
    }
}
