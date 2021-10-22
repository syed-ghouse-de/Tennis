using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Entity
{
    /// <summary>
    /// Entity list to maintain the list of entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityList<T> : List<T> where T : IEntity
    {        
    }
}
