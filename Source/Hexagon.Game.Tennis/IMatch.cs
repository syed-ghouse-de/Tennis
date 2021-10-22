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
        /// To start the match
        /// </summary>
        void Start();

        /// <summary>
        /// Start playing the match
        /// </summary>
        void Play();

        /// <summary>
        /// Stop the match
        /// </summary>
        void Stop();

    }
}
