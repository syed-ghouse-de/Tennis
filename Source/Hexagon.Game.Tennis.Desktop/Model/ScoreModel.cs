using Hexagon.Game.Framework.MVVM.ViewModel;
using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Model
{
    /// <summary>
    /// Score model
    /// </summary>
    public class ScoreModel : ObservableModel
    {
        public MatchModel Match { get; set; }
        public List<PlayerModel> Players { get; set; }        
        public Dictionary<Guid, string> Sets { get; set; }
        public Dictionary<Guid, List<GameModel>> Games { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public ScoreModel()
        {
            Match = new MatchModel();
            Players = new List<PlayerModel>();
         
            Sets = new Dictionary<Guid, string>();
            Games = new Dictionary<Guid, List<GameModel>>();
        }
    }
}
