using Autofac;
using Autofac.Core;
using Hexagon.Game.Framework.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.DependencyInjection
{
    /// <summary>
    /// Dependency injection container
    /// </summary>
    public class DependencyInjection
    {
        private IContainer _container = null;
        private ContainerBuilder _containerBuilder = null;

        // member variable to have single instance
        private static DependencyInjection _instance;

        /// <summary>
        /// Private constructor
        /// </summary>
        private DependencyInjection()
        {
            // Create an instanc of container builder
            _containerBuilder = new ContainerBuilder();
        }

        /// <summary>
        /// Get the instance of dependency injection
        /// </summary>
        public static DependencyInjection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DependencyInjection();

                return _instance;
            }
        }

        /// <summary>
        /// Register Ioc modules
        /// </summary>
        /// <typeparam name="T">Service module to register</typeparam>
        /// <returns></returns>
        public DependencyInjection RegisterModule<T>() where T : IModule, new()
        {
            _containerBuilder.RegisterModule<T>();
            return this;
        }

        /// <summary>
        /// Build the container
        /// </summary>
        public void Build()
        {
            _container = _containerBuilder.Build();
        }

        /// <summary>
        /// Get the container instance
        /// </summary>
        public IContainer Container { get { return _container; } }
    }
}
