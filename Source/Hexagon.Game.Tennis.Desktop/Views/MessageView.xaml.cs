﻿using Hexagon.Game.Framework.MVVM.View;
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
    /// Interaction logic for MessageViewModel.xaml
    /// </summary>
    public partial class MessageView : Window, IView
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize default properties specific to view
        /// </summary>
        public void Initialize()
        { }
    }
}
