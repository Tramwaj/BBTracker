using BBTracker.Models;
//using BBTracker.Models.PlayTypes;
using BBTracker.Services;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BBTracker.ViewModels
{
    class CurrentGameViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public Game Game { get; set; }
        public List<Player> Players { get; set; }
        public ObservableCollection<Play> Plays { get; set; }
        public List<PlayerGame> PlayerGames { get; set; }

        public List<Player> TeamA { get; set; }
        public ObservableCollection<Player> TeamAOnCourt { get; set; }
        public ObservableCollection<Player> TeamABench { get; set; }
        public List<Player> TeamB { get; set; }
        public ObservableCollection<Player> TeamBOnCourt { get; set; }
        public ObservableCollection<Player> TeamBBench { get; set; }

        public bool PossesionTeamB { get; set; }
        public int TeamSize { get; set; }
        private Play _play;
        private PlayCreator _playFactory;

        public string InfoText { get; set; }
        private IDataAccess _dataAccess;

        public ObservableCollection<PlayerStatsCurrentGame> StatsListTeamA { get; set; }
        public CurrentStatsService Stats { get; set; }
        public ObservableCollection<PlayerStatsCurrentGame> StatsListTeamB { get; set; }

        public CurrentGameViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeViewModel();
        }

        private async void InitializeViewModel()
        {
            _dataAccess.OpenConnection();
            Players = await _dataAccess.GetPlayersAsyncAlreadyConnected();
            InitializeCommands();
            CrudeTeamSettings();
            //SetDefaultButtonsVisible();
            SetZeroVisibility();
            GameNotStarted = true;
            StatsListTeamA = new ObservableCollection<PlayerStatsCurrentGame>();
            StatsListTeamB = new ObservableCollection<PlayerStatsCurrentGame>();
            Stats = new CurrentStatsService(StatsListTeamA, StatsListTeamB);
        }

        private void UpdateStatsHandler(object sender, NotifyCollectionChangedEventArgs args)
        {
            foreach (var play in args.NewItems)
            {
                Stats.AddPlay(play as Play);
            }
            //StatsService.AddPlays(new List<Play>(args.NewItems));
        }

        //NotifyCollectionChangedEventHandler UpdateStatsHandler;


        private void CrudeTeamSettings() //TODO: change to sth meaningfull
        {
            TeamA = Players.Take(4).ToList();
            TeamB = Players.TakeLast(4).ToList();

            TeamAOnCourt = new ObservableCollection<Player>(TeamA.Take(3).ToList());
            TeamBOnCourt = new ObservableCollection<Player>(TeamB.Take(3).ToList());
            TeamABench = new ObservableCollection<Player>(TeamA.TakeLast(1).ToList());
            TeamBBench = new ObservableCollection<Player>(TeamB.TakeLast(1).ToList());
        }

        private void InitializeCommands()
        {
            StartGameCommand = new RelayCommand(StartGame);
            EndGameCommand = new RelayCommand(EndGame);
            CancelActionCommand = new RelayCommand(CancelAction);
            SwitchPossesionCommand = new RelayCommand(SwitchPossession);
            ChoosePlayCommand = new RelayCommand<string>((choice) => ChoosePlay(choice));
            ChoosePlayerCommand = new RelayCommand<int>((id) => ChoosePlayer(id));

            ChoosePointsCommand = new RelayCommand<string>((points) => ChoosePoints(points));
            NoAssistCommand = new RelayCommand(NoAssist);
            FieldGoalOutcomeCommand = new RelayCommand<string>((outcome) => FieldGoalOutcome(outcome));
            NoReboundCommand = new RelayCommand(NoRebound);
            NoStealCommand = new RelayCommand(NoSteal);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        public ICommand SwitchPossesionCommand { get; private set; }
        private void SwitchPossession()
        {
            PossesionTeamB = !PossesionTeamB;
            SetPlayersVisibility();
        }

        public bool GameNotStarted { get; set; }
        public bool GameStarted { get; set; }

        public ICommand StartGameCommand { get; private set; }
        private void StartGame()
        {
            Game = new Game();
            Plays = new ObservableCollection<Play>();
            _playFactory = new PlayCreator(Game);

            Plays.CollectionChanged += UpdateStatsHandler;

            AddPlayerGames();
            CheckInPlayers();

            SetDefaultButtonsVisibleOnly();
            SetPlayersVisibility();
            GameNotStarted = false;
            GameStarted = true;
        }

        private void AddPlayerGames()
        {
            PlayerGames = new List<PlayerGame>();

            foreach (var player in TeamA)
            {
                PlayerGames.Add(new PlayerGame
                {
                    Game = Game,
                    Player = player,
                    TeamB = false
                });
            }
            foreach (var player in TeamB)
            {
                PlayerGames.Add(new PlayerGame
                {
                    Game = Game,
                    Player = player,
                    TeamB = true
                });
            }
        }

        public ICommand EndGameCommand { get; private set; }
        private void EndGame()
        {
            Game.End = DateTime.Now;
            CheckOutPlayers();
            Game.Plays = Plays;
            Game.PlayerGames = PlayerGames;
            Game.ScoreB = Plays.Where(p => p.TeamB && p.MadeFG).Sum(p => p.Points);
            Game.ScoreA = Plays.Where(p => !p.TeamB && p.MadeFG).Sum(p => p.Points);

            _dataAccess.AddGameAsync(Game);
            _dataAccess.AddPlaysAsync(Plays);
            _dataAccess.AddPlayerGames(PlayerGames);
            _dataAccess.SaveChangesAsync();

            GameNotStarted = true;
            GameStarted = false;
        }

        private void CheckInPlayers()
        {
            foreach (Player player in TeamAOnCourt)
            {
                _playFactory.NewPlay(player, false);
                _playFactory.ChoosePlayType(PlayType.CheckIn);
                Plays.Add(_playFactory.GetPlays().First());
                _playFactory.Clear();
            }
            foreach (Player player in TeamBOnCourt)
            {
                _playFactory.NewPlay(player, true);
                _playFactory.ChoosePlayType(PlayType.CheckIn);
                Plays.Add(_playFactory.GetPlays().First());
                _playFactory.Clear();
            }
        }

        private void CheckOutPlayers()
        {
            foreach (Player player in TeamAOnCourt)
            {
                _playFactory.NewPlay(player, false);
                _playFactory.ChoosePlayType(PlayType.CheckOut);
                Plays.Add(_playFactory.GetPlays().First());
                _playFactory.Clear();
            }
            foreach (Player player in TeamBOnCourt)
            {
                _playFactory.NewPlay(player, true);
                _playFactory.ChoosePlayType(PlayType.CheckOut);
                Plays.Add(_playFactory.GetPlays().First());
                _playFactory.Clear();
            }
        }

        public ICommand CancelActionCommand { get; private set; }
        private void CancelAction()
        {
            _play = null;
            _playFactory.Clear();
            SetDefaultButtonsVisibleOnly();
            SetPlayersVisibility();
        }

        public ICommand ChoosePlayerCommand { get; set; }
        private void ChoosePlayer(int Id)
        {
            var _player = Players.First(p => p.Id == Id);
            if (!_playFactory.HasPlay)
            {
                SetZeroVisibility();
                SetDefaultButtonsVisibleOnly();
                //_play = NewPlayNow(Players.First(p => p.Id == Id), PossesionTeamB);
                _playFactory.NewPlay(_player, PossesionTeamB);
                InfoText = "Wybierz akcję:";

            }
            else
            {
                //_play.Player = Players.First(p => p.Id == Id);
                _playFactory.ConsecutivePlay(_player, TeamB.Contains(_player));
                foreach (var play in _playFactory.GetPlays())
                {
                    Plays.Add(play);

                    if (play.PlayType == PlayType.CheckIn)
                    {
                        if (TeamABench.Contains(play.Player))
                        {
                            TeamABench.Remove(play.Player);
                            TeamAOnCourt.Add(play.Player);
                        }
                        if (TeamBBench.Contains(play.Player))
                        {
                            TeamBBench.Remove(play.Player);
                            TeamBOnCourt.Add(play.Player);
                        }

                    }
                    if (play.PlayType == PlayType.CheckOut)
                    {
                        if (TeamAOnCourt.Contains(play.Player))
                        {
                            TeamAOnCourt.Remove(play.Player);
                            TeamABench.Add(play.Player);
                        }
                        if (TeamBOnCourt.Contains(play.Player))
                        {
                            TeamBOnCourt.Remove(play.Player);
                            TeamBBench.Add(play.Player);
                        }
                    }
                };

                if (Plays.Last().PlayType == PlayType.Rebound)
                    PossesionTeamB = Plays.Last().TeamB;
                _playFactory.Clear();

                //switch possesion dla zbiorki nieofensywnej
                InfoText = "Wybierz zawodnika";
                SetDefaultButtonsVisibleOnly();
                SetPlayersVisibility();
            }
        }

        public ICommand ChoosePlayCommand { get; set; }
        private void ChoosePlay(string choice)
        {
            DefaultButtonsVisibility = Visibility.Collapsed;
            TeamAPlayersActive = false;
            TeamBPlayersActive = false;
            switch (choice)
            {
                case "FieldGoal":
                    _playFactory.ChoosePlayType(PlayType.FieldGoal);
                    ChoosePointsVisibility = Visibility.Visible;
                    InfoText = "Za ile punktów rzut?";
                    break;
                case "Turnover":
                    _playFactory.ChoosePlayType(PlayType.TurnOver);
                    SwitchPossession();
                    ChooseTurnoverCauserVisibility = Visibility.Visible;
                    SetPlayersVisibility(false);
                    InfoText = "Czy ktoś przejął piłkę?";
                    break;
                case "Substitution":
                    _playFactory.ChoosePlayType(PlayType.CheckOut);

                    SetBenchPlayersVisibility();
                    InfoText = "Za kogo zmiana?";
                    break;
            }

        }

        public ICommand ChoosePointsCommand { get; set; }
        private void ChoosePoints(string points)
        {
            _playFactory.SelectPoints(points);

            ChoosePointsVisibility = Visibility.Collapsed;
            ChooseFieldGoalOutcomeVisibility = Visibility.Visible;
            InfoText = "Jaki wynik rzutu?";
        }

        public ICommand FieldGoalOutcomeCommand { get; set; }
        private void FieldGoalOutcome(string outcome)
        {
            ChooseFieldGoalOutcomeVisibility = Visibility.Collapsed;
            _playFactory.FGResult(outcome);
            switch (outcome)
            {
                case "Make":
                    MadeFieldGoal();
                    break;
                case "Miss":
                    MissedFieldGoal();
                    break;
                case "Block":
                    BlockedFieldGoal();
                    break;
            }
        }

        private void MadeFieldGoal()
        {
            SwitchPossession();
            ChooseAssisterVisibility = Visibility.Visible;
            SetPlayersVisibility(true); //possesion changed, so to look for a player from the same team - we have to set 'otherteam' to true
            InfoText = "Kto asystował?";
        }
        private void FlushPlay()
        {
            Plays.Add(_playFactory.GetPlays().Last());
            _playFactory.Clear();
        }

        public ICommand NoAssistCommand { get; set; }
        private void NoAssist()
        {
            FlushPlay();
            SetDefaultButtonsVisibleOnly();
            SetPlayersVisibility();
            InfoText = "Wybierz zawodnika";
        }

        public ICommand NoStealCommand { get; set; }
        private void NoSteal()
        {
            FlushPlay();
            SetDefaultButtonsVisibleOnly();
            SetPlayersVisibility();
            InfoText = "Wybierz zawodnika";
        }

        private void MissedFieldGoal()
        {
            InfoText = "Kto zebrał?";
            ChooseRebounderVisibility = Visibility.Visible;
            SetAllPlayersVisibility();
        }

        public ICommand NoReboundCommand { get; set; }
        private void NoRebound()
        {
            FlushPlay();
            SwitchPossession();
            SetDefaultButtonsVisibleOnly();
            InfoText = "Wybierz zawodnika";
        }

        private void BlockedFieldGoal()
        {
            InfoText = "Kto zablokował rzut?";
            SetPlayersVisibility(true);
        }

        private Play NewPlayFrom(Play _play, PlayType _playType, bool _sameTeam)
        {
            return new Play
            {
                PlayType = _playType,
                Time = _play.Time,
                GameTime = _play.GameTime,
                Points = _play.Points != 0 ? _play.Points : 0,
                TeamB = _sameTeam ? _play.TeamB : !_play.TeamB
            };
        }

        private Play NewPlayNow(Player player, bool teamB)
        {
            return new Play
            {
                Player = player,
                Time = DateTime.Now,
                GameTime = DateTime.Now - Game.Start,
                TeamB = teamB
            };
        }

        public ICommand CloseWindowCommand { get; private set; }
        private void CloseWindow()
        {
            _dataAccess.CloseConnectionAsync();
        }

        private void SetPlayersVisibility(bool otherTeam = false)
        {
            if ((PossesionTeamB && otherTeam) ||
                (!PossesionTeamB && !otherTeam))
            {
                TeamAPlayersActive = true;
                TeamBPlayersActive = false;
            }
            else
            {
                TeamAPlayersActive = false;
                TeamBPlayersActive = true;
            }
            //TeamAPlayersActive = true;
            //TeamBPlayersActive = true;
            DefaultButtonsActive = false;
            TeamBBenchActive = false;
            TeamBBenchActive = false;
        }

        private void SetAllPlayersVisibility()
        {
            TeamAPlayersActive = true;
            TeamBPlayersActive = true;
            InfoText = "Wybierz zawodnika";
        }

        private void SetBenchPlayersVisibility()
        {
            if (PossesionTeamB)
            {
                TeamBBenchActive = true;
            }
            else
            {
                TeamABenchActive = true;
            }
            InfoText = "Wybierz zawodnika";
        }

        private void SetDefaultButtonsVisibleOnly()
        {
            DefaultButtonsVisibility = Visibility.Visible;
            ChoosePointsVisibility = Visibility.Collapsed;
            ChooseFieldGoalOutcomeVisibility = Visibility.Collapsed;
            ChooseTurnoverCauserVisibility = Visibility.Collapsed;
            ChooseRebounderVisibility = Visibility.Collapsed;
            ChooseAssisterVisibility = Visibility.Collapsed;
            DefaultButtonsActive = true;
            TeamABenchActive = false;
            TeamBBenchActive = false;
        }

        private void SetZeroVisibility()
        {
            DefaultButtonsVisibility = Visibility.Collapsed;
            ChoosePointsVisibility = Visibility.Collapsed;
            ChooseFieldGoalOutcomeVisibility = Visibility.Collapsed;
            ChooseTurnoverCauserVisibility = Visibility.Collapsed;
            ChooseRebounderVisibility = Visibility.Collapsed;
            ChooseAssisterVisibility = Visibility.Collapsed;
            TeamABenchActive = false;
            TeamBBenchActive = false;
            TeamAPlayersActive = false;
            TeamBPlayersActive = false;
        }



        public Visibility DefaultButtonsVisibility { get; set; }
        public Visibility ChoosePointsVisibility { get; set; }
        public Visibility ChooseFieldGoalOutcomeVisibility { get; set; }
        public Visibility ChooseTurnoverCauserVisibility { get; set; }
        public Visibility ChooseRebounderVisibility { get; set; }
        public Visibility ChooseAssisterVisibility { get; set; }
        public bool TeamAPlayersActive { get; set; } //which players are active for a play
        public bool TeamBPlayersActive { get; set; }
        public bool TeamABenchActive { get; set; } //which players are active for a play
        public bool TeamBBenchActive { get; set; }
        public bool DefaultButtonsActive { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;


    }
    //internal class PlayerStats
    //{
    //    public int Points { get; set; }
    //    public int Rebounds { get; set; }
    //    public int Assists { get; set; }

    //}
}
