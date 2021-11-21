using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.DependencyInjection
{ 
    /// <summary>
    /// Abstract class to extend the service module registration
    /// </summary>
    public abstract class DependencyInjectionModule : Module
    {
        /// <summary>
        /// Override load method to register the services
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder) { }
    }
}
