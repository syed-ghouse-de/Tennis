using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Model
{
    /// <summary>
    /// Game model
    /// </summary>
    public class GameModel
    {
        public PlayerModel Server { get; set; }
        public PlayerModel Receiver { get; set; }
    }
}
