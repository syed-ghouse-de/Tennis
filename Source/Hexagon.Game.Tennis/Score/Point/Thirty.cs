using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Score
{
    /// <summary>
    /// Class to maintain the Thirty point state
    /// </summary>
    public class Thirty : BasePoint, IPoint
    {
        /// <summary>
        /// Execute to maintain the state of player loose point
        /// </summary>
        public IPoint Loose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute to maintain the state of player win point
        /// </summary>
        public IPoint Win()
        {
            throw new NotImplementedException();
        }
    }
}
