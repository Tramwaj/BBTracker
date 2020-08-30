using BBTracker.Models;
using BBTracker.Services;
using GalaSoft.MvvmLight.Command;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BBTracker.ViewModels
{
    class GamesHistoryViewModel : INotifyPropertyChanged
    {
        public List<Game> Games { get; set; }
        public Game Game { get; set; }
        public ICollection<Play> Plays { get; set; }
        public GameStatsService GameStats { get; set; }
        public PlayersStatsService PlayerStats { get; set; }
        private IDataAccess DataAcess;

        public GamesHistoryViewModel()
        {
            DataAcess = new DBAccessService();
            GetGamesAsync();
            //ChooseGameCommand = new RelayCommand<Game>((game) => ChooseGame(game));
            this.PropertyChanged += GamesHistoryViewModel_PropertyChanged;
        }

        private async void GamesHistoryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Game")
            {
                Plays = await GetGamePlaysAsync();

                GameStats = new GameStatsService(Plays);
                PlayerStats = new PlayersStatsService(Plays);
            }
        }

        public async void GetGamesAsync()
        {
            Games = await DataAcess.GetAllGamesAsync();
        }
        
        private async Task<ICollection<Play>> GetGamePlaysAsync()
        {
            return await DataAcess.GetPlaysFromGameAsync(Game.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

        //public ICommand ChooseGameCommand { get; set; }
        //private async void ChooseGame(Game game)
        //{
        //    Game = game;
        //    Plays = await DataAccessService.GetPlaysFromGameAsync(game.Id);
        //}

        //private async Task<ICollection<Play>> GetGamePlaysAsync()
        //{
        //    return await DataAccessService.GetPlaysFromGameAsync(Game.Id);
        //}