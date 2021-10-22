using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis
{
    /// <summary>
    /// Player template 
    /// </summary>
    public interface IPlayer
    {
        PlayerEntity Identity { get; }

        void Win();
        void Loose();
    }
}
