using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hexagon.Game.Tennis.Desktop.Command
{
    /// <summary>
    /// Relay command class for handling commands
    /// </summary>
    public class RelayCommand : ICommand
    {        
        // Action & Predicate for command execution
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;  
        
        /// <summary>
        /// Parameterized constructors
        /// </summary>
        /// <param name="execute">Action for an execution</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        /// <summary>
        /// Parameterized constructors
        /// </summary>
        /// <param name="execute">Action for an execution</param>
        /// <param name="canExecute">Predicate for execute</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute; _canExecute = canExecute;
        }
       
        /// <summary>
        /// Check for execution
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Return True if it can be executed, otherwise returns False</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Event hadler for change execute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Execute a command
        /// </summary>
        /// <param name="parameter">Parameter</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Execute a command if it can be executed
        /// </summary>
        /// <param name="parameter">Parameter</param>
        public void CheckAndExecute(object parameter)
        {
            if (CanExecute(parameter)) Execute(parameter);
        }
    }
}
