using Autofac;
using Hexagon.Game.Framework.DependencyInjection;
using Hexagon.Game.Framework.Extension;
using Hexagon.Game.Framework.MVVM.View;
using Hexagon.Game.Framework.MVVM.ViewModel;
using Hexagon.Game.Tennis.Desktop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Desktop.ViewModels
{
    /// <summary>
    /// Abstract base class for view model
    /// </summary>
    public abstract class BaseViewModel : ObservableModel
    {        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseViewModel()
        {
            try
            {
                // Get the view for the current view model 
                View = DependencyInjection.Instance
                    .Container.ResolveNamed<IView>(ViewName);
                if (View != null)
                {
                    View.DataContext = this;
                    View.Initialize();
                }
            }
            catch (Exception exception) { }
        }

        // Get the view for a view model
        public IView View { get; protected set; }

        // Extract the View name
        public string ViewName
        {
            get
            {
                // Get the nme of view
                string name = this.GetType().Name;
                return name.Remove(name.IndexOf("Model"));
            }
        }
    }
}
