using Autofac;
using Hexagon.Game.Framework.DependencyInjection;
using Hexagon.Game.Framework.MVVM.View;
using Hexagon.Game.Tennis.Desktop.Handler;
using Hexagon.Game.Tennis.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.Service
{
    /// <summary>
    /// Dependency injection service module
    /// </summary>
    public class ServiceModule : DependencyInjectionModule
    {
        /// <summary>
        /// Method to register all the required services
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // Register Views
            RegisterViews(builder);

            builder.RegisterType<MatchHandler>()
                .As<IMatchHandler>()
                .SingleInstance();

            // Register dependency services
            builder
                .RegisterType<SpectatorScoreViewModel>()
                .InstancePerDependency();
            builder
                .RegisterType<RefereeScoreViewModel>()
                .InstancePerDependency();
        }

        /// <summary>
        /// Register views for loading dynamicaly when ViewModel is called
        /// </summary>
        /// <param name="builder">Container to register the services</param>
        private void RegisterViews(ContainerBuilder builder)
        {
            // Iterate all the assemblies ending with View to register in the container
            foreach (var type in Assembly.GetAssembly(typeof(App)).GetTypes())
            {
                // If the interfce is of type IView and ending with View register
                if (type.GetInterfaces().Contains(typeof(IView)))
                {
                    if (type.Name.EndsWith("View", StringComparison.Ordinal))
                        builder.RegisterType(type).Named<IView>(type.Name);
                }
            }
        }
    }
}
