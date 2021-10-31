using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Entity
{
    public class StatusEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MatchEntity> Match { get; set; }
    }
}
