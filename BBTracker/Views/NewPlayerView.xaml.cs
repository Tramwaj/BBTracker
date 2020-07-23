using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BBTracker.Models;
using MahApps.Metro.Controls;

namespace BBTracker.Views
{
    /// <summary>
    /// Interaction logic for AddPlayerView.xaml
    /// </summary>
    public partial class NewPlayerView : MetroWindow
    {

        public NewPlayerView()
        {
            InitializeComponent();
            DataContext = new Player();
        }
        public NewPlayerView(Player player)
        {
            InitializeComponent();
            DataContext = player;
        }
    }
}
