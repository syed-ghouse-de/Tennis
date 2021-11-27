using Hexagon.Game.Framework.MVVM.Command;
using Hexagon.Game.Tennis.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hexagon.Game.Tennis.Desktop.ViewModels
{
    /// <summary>
    /// Message view model for managing warning and error messages
    /// </summary>
    public class MessageViewModel : BaseViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageViewModel()
        { }

        /// <summary>
        /// On click close the window
        /// </summary>
        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null) _okCommand =
                        new RelayCommand(new Action<object>(OnOkCommand));
                return _okCommand;
            }
            set { SetProperty(ref _okCommand, value); }
        }

        /// <summary>
        /// Message to display on screen
        /// </summary>
        private MessageModel _message = new MessageModel();
        public MessageModel Message
        {
            get { return _message; }
            set { SetPropertyAndNotify(ref _message, value, () => Message); }
        }

        /// <summary>
        /// On click to close the window
        /// </summary>
        /// <param name="obj"></param>
        private void OnOkCommand(object obj)
        {
            // Close the window
            this.View.Close();        
        }
    }
}
