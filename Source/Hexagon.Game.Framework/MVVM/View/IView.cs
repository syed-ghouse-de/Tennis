using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.MVVM.View
{
    /// <summary>
    /// View interface for managing the link between View & View Model
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Data Context
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Initialize default properties specific to view
        /// </summary>
        void Initialize();

        /// <summary>
        /// Show non-modal window
        /// </summary>
        void Show();

        /// <summary>
        /// Show modal window
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();

        /// <summary>
        /// Close window
        /// </summary>
        void Close();
    }
}
