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
    /// Player model
    /// </summary>
    public class PlayerModel : PlayerEntity
    {        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerModel()
        { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public PlayerModel(PlayerEntity player)
        {
            Id = player.Id;
            FirstName = player.FirstName;
            SurName = player.SurName;
            LastName = player.LastName;
            DateOfBirth = player.DateOfBirth;
            Club = player.Club;
        }

        public List<string> Sets { get; set; }
        public Dictionary<Guid, int> GamesWon { get; set; }
        public string Point { get; set; } 
        public string Server { get; set; }
    }
}
