using BBTracker.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BBTracker.ViewModels
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            GamesHistoryCommand = new RelayCommand(GamesHistory);
            NewGameCommand = new RelayCommand(NewGame);
            PlayerListCommand = new RelayCommand(PlayerList);
            NewPlayerCommand = new RelayCommand(NewPlayer);
        }

        public ICommand GamesHistoryCommand { get; set; }
        private void GamesHistory()
        {
            GamesHistoryView historyView = new GamesHistoryView();
            historyView.Show();
        }

        public ICommand NewGameCommand { get; private set; }
        private void NewGame()
        {
            CurrentGameView gameview = new CurrentGameView();
            gameview.Show();
        }

        public ICommand PlayerListCommand { get; private set; }
        private void PlayerList()
        {
            PlayerListView playerListView = new PlayerListView();
            playerListView.Show();
        }

        public ICommand NewPlayerCommand { get; set; }
        private void NewPlayer()
        {
            NewPlayerView newPlayerView = new NewPlayerView();
            newPlayerView.Show();
        }

    }
}

