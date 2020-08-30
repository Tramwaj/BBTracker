using BBTracker.Services;
using BBTracker.ViewModels;
using MahApps.Metro.Controls;
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

namespace BBTracker.Views
{
    /// <summary>
    /// Interaction logic for GamesHistoryView.xaml
    /// </summary>
    public partial class GamesHistoryView : MetroWindow
    {
        public GamesHistoryView(IDataAccess dataAccess)
        {
            InitializeComponent();
            DataContext = new GamesHistoryViewModel(dataAccess);
        }
    }
}
