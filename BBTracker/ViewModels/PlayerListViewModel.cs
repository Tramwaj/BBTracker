using BBTracker.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BBTracker.ViewModels
{
    public class PlayerListViewModel : INotifyPropertyChanged
    {
        //public List<Player> Players { get; set; }
        //public BBTrackerContext _context = new BBTrackerContext();

        public ObservableCollection<Player> Players { get; set; }
        private BBTrackerContext _context;       
        

        //public event NotifyCollectionChangedEventHandler CollectionChanged;
        //public event PropertyChangedEventHandler PropertyChanged;
               

        public PlayerListViewModel()
        {
            _context = new BBTrackerContext();

            Players = new ObservableCollection<Player> ( _context.Players );

            AddOneCommand = new RelayCommand(AddOne);
            SavePlayersCommand = new RelayCommand(SavePlayers);
        }

        public ICommand AddOneCommand { get; private set; }
        private void AddOne()
        {
            Players.Add(new Player { FirstName = "jan", LastName = "nowak" });
        }

        public ICommand SavePlayersCommand { get; private set; }
        private void SavePlayers()
        {
            _context = new BBTrackerContext();
            _context.Players.RemoveRange(_context.Players.Where(p => p.Nick != "PJDRN"));
            _context.Players.AddRange(Players);
            _context.SaveChanges();            
        }
       


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
