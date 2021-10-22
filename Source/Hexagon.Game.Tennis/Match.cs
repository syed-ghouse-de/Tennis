using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Tennis.Entity;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Match class to contains match associated attributes
    /// </summary>
    public class Match : IMatch
    {
        /// <summary>
        ///  Private memeber variables
        /// </summary>
        public string Name { get; set; }                            
        public string Court { get; set; }
        public  DateTime Date { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public Status Status { get; set; }         
        
        public Players Players { get; set; }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// To stop the match
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
