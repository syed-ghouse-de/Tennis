using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Game.Framework.Entity;

namespace Hexagon.Game.Tennis.Entity
{
    public class PlayerEntity : IEntity
    {
        /// <summary>
        /// Player entity member variables
        /// </summary>
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Club { get; set; }

        /// <summary>
        ///  Default constructor
        /// </summary>
        public PlayerEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Parametarized constructor
        /// </summary>
        /// <param name="id"></param>
        public PlayerEntity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Parametarized constructor 
        /// </summary>
        /// <param name="entity">Player entity</param>
        public PlayerEntity(PlayerEntity entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            SurName = entity.SurName;
            LastName = entity.LastName;
            DateOfBirth = entity.DateOfBirth;
            Club = entity.Club;
        }
    }
}
