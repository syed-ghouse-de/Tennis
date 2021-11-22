using Autofac;
using Hexagon.Game.Framework.DependencyInjection;
using Hexagon.Game.Framework.MVVM.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hexagon.Game.Tennis.Desktop.ViewModels
{
    /// <summary>
    /// Main view odel
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel() 
        { }

        /// <summary>
        /// Spectator Command
        /// </summary>
        private ICommand spectatorCommand;
        public ICommand OnSpectatorCommand
        {
            get
            {
                if (spectatorCommand == null) spectatorCommand = new RelayCommand(new Action<object>(OnSpectatorClick));
                return spectatorCommand;
            }
            set { SetProperty(ref spectatorCommand, value); }
        }

        /// <summary>
        /// On Spectator click
        /// </summary>
        /// <param name="obj">Source object</param>
        private void OnSpectatorClick(object obj)
        {
            // Resolve the view model fro IoC
            var view = DependencyInjection.Instance
                .Container.Resolve<SpectatorScoreViewModel>();

            view.View.Show();
        }

        /// <summary>
        /// Referee Command
        /// </summary>
        private ICommand refereeCommand;
        public ICommand OnRefereeCommand
        {
            get
            {
                if (refereeCommand == null) refereeCommand = new RelayCommand(new Action<object>(OnRefereeClick));
                return refereeCommand;
            }
            set { SetProperty(ref refereeCommand, value); }
        }

        /// <summary>
        /// Referee command click
        /// </summary>
        /// <param name="obj">Source object</param>
        private void OnRefereeClick(object obj)
        {
            // Resolve the view model fro IoC
            var view = DependencyInjection.Instance
                .Container.Resolve<RefereeScoreViewModel>();

            view.View.Show();
        }
    }
}
