using Hexagon.Game.Framework.MVVM.View;
using Hexagon.Game.Tennis.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hexagon.Game.Tennis.Desktop.Views
{
    /// <summary>
    /// Interaction logic for SpectatorScoreView.xaml
    /// </summary>
    public partial class SpectatorScoreView : Window, IView
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpectatorScoreView()
        {
            InitializeComponent();                       
        }

        /// <summary>
        /// Initialize default properties specific to view
        /// </summary>
        public void Initialize()
        {
            GenerateGridColumns();
        }

        /// <summary>
        /// Generate grid columns
        /// </summary>
        private void GenerateGridColumns()
        {
            AddGridColumn("Name", "FirstName");
            AddGridColumn(" ", "Server");
            for (int sets = 0; sets < 5; sets++)
                AddGridColumn(String.Format("Set {0}", sets + 1), String.Format("Sets[{0}]", sets));
            AddGridColumn(string.Empty, string.Empty);
            AddGridColumn("Point", "Point");
        }

        /// <summary>
        /// Add grid columns
        /// </summary>
        /// <param name="header">Header of the column</param>
        /// <param name="binding">Name of the column to bind</param>
        private void AddGridColumn(string header, string binding)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = header;         
            if (!header.Equals(string.Empty)) column.Binding = new Binding(binding);
            dataGrid.Columns.Add(column);
        }
    }
}
