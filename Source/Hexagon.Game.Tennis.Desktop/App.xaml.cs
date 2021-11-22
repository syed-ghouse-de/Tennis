using Hexagon.Game.Framework.DependencyInjection;
using Hexagon.Game.Tennis.Desktop.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hexagon.Game.Tennis.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Registation of dependency injection services
            DependencyInjection.Instance
                .RegisterModule<ServiceModule>().Build();
        }

    }
}
