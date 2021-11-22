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
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            MainWindowViewModel model = new MainWindowViewModel();
            DataContext = model;
        }

        public object Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
