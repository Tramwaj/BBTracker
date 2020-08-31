using BBTracker.DB;
using BBTracker.Services;
using BBTracker.Views;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BBTracker.ViewModels
{
    class MainWindowViewModel
    {
        private IDataAccess dataAccess; 
        public MainWindowViewModel()
        {
            GamesHistoryCommand = new RelayCommand(GamesHistory);
            NewGameCommand = new RelayCommand(NewGame);
            PlayerListCommand = new RelayCommand(PlayerList);
            NewPlayerCommand = new RelayCommand(NewPlayer);
            AskForDemoMessageBox();
        }

        private void AskForDemoMessageBox()
        {
            string msgText = "Czy chcesz uruchomić wersję demonstracyjną?";
            string title = "Wersja demo?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgText, title, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dataAccess = new Serializer();
                    break;
                case MessageBoxResult.No:
                    dataAccess = new DBAccessService();
                    break;
            }
        }

        public ICommand GamesHistoryCommand { get; set; }
        private void GamesHistory()
        {
            GamesHistoryView historyView = new GamesHistoryView(dataAccess);
            historyView.Show();
        }

        public ICommand NewGameCommand { get; private set; }
        private void NewGame()
        {
            CurrentGameView gameview = new CurrentGameView(dataAccess);
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

