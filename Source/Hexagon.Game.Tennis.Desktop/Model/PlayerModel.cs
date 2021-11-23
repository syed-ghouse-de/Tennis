using Hexagon.Game.Framework.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Model
{
    /// <summary>
    /// Player model
    /// </summary>
    public class PlayerModel : ObservableModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Club { get; set; }
        public List<string> Sets { get; set; }
        public Dictionary<Guid, int> GamesWon { get; set; }
        public string Point { get; set; } 
        public string Server { get; set; }
    }
}
