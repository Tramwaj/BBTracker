//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro.Controls;
using BBTracker.Views;
using BBTracker.Services;
using GalaSoft.MvvmLight.Command;

namespace BBTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly GamesListService _gamesListService = new GamesListService();
        public MainWindow()
        {
            InitializeComponent();

            //dev TEMP:

            GameView gameview = new GameView();
            gameview.Show();

            //To be brought back:
            // DataContext = InitializeContext();

            //DataContext = _gamesListService.GetGameIds("2019");

        }
        //private async Task<BBTrackerContext> InitializeContext()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json");

        //    var configuration = builder.Build();

        //    var connectionString = configuration.GetConnectionString("BBTrackerConnectionString");

        //    var context = new BBTrackerContext(connectionString);
        //    return context;
        //}

        public ICommand NewGameCommand { get; private set; }
        private void NewGame()
        {
            GameView gameview = new GameView();
            gameview.Show();
        }

        private void PlayersButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerListView playerListView = new PlayerListView();
            playerListView.Show();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerView newPlayerView = new NewPlayerView();
            newPlayerView.Show();            
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }


        //private void GetNBA2020Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var _gamesService = new GamesListService();
        //    GamesListBox.ItemsSource = _gamesService.GetGameIds();
        //}
    }
}
