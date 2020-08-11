
using MahApps.Metro.Controls;
using BBTracker.Services;
using BBTracker.ViewModels;

namespace BBTracker
{
    public partial class MainWindow : MetroWindow
    {
        private readonly NBAGamesListService _gamesListService = new NBAGamesListService();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();           
            

        }
        
    }
}
