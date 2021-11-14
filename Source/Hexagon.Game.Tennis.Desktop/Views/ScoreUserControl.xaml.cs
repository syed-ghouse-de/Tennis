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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hexagon.Game.Tennis.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ScoreUserControl.xaml
    /// </summary>
    public partial class ScoreUserControl : UserControl
    {
        public ScoreUserControl()
        {
            InitializeComponent();
            DataContext = new ScoreViewModel();

            GenerateGridColumns();
        }
        private void GenerateGridColumns()
        {
            AddGridColumn("Name", "FirstName");
            for (int sets = 0; sets < ((ScoreViewModel)DataContext).MatachHandler.Match.BestOfSets; sets++)
                AddGridColumn(String.Format("Set {0}", sets + 1), String.Format("Sets[{0}]", sets));
            AddGridColumn(string.Empty, string.Empty);
            AddGridColumn("Point", "Point");
        }

        private void AddGridColumn(string header, string binding)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = header;
            if (!header.Equals(string.Empty)) column.Binding = new Binding(binding);
            dataGrid.Columns.Add(column);
        }
    }
}
