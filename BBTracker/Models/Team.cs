using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Policy;
using System.Text;

namespace BBTracker.Models
{
    class Team : INotifyCollectionChanged
    {
        public ICollection<Player> Players { get; private set; }
        public ICollection<Player> OnCourt { get; private set; }
        public ICollection<Player> Bench { get; private set; }
        private Player PlayerBenched;


        public int TeamSize { get; private set; }
        public Team(int teamsize) => TeamSize = teamsize;

        public void SetTeamSize(int teamsize)
        {
            TeamSize = teamsize; 
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
            if (OnCourt.Count < TeamSize)
            {
                OnCourt.Add(player);
            }
            else
            {
                Bench.Add(player);
            }
        }
        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
        public void CheckOut(Player player)
        {
            PlayerBenched = player;
        }
        public void CheckIn(Player player)
        {
            OnCourt.Remove(PlayerBenched);
            Bench.Remove(player);
            OnCourt.Add(player);
            Bench.Add(PlayerBenched);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}