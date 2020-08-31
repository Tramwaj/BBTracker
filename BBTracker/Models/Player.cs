using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BBTracker.Models
{
    [Serializable]
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nick { get; set; }
        public int? Number { get; set; }

        public string FullName { get { return FirstName + " " + Nick + " " + LastName; }}
        //public string ShortName { get { return FirstName + " " + Nick + " " + LastName[0]; } }
        public virtual ICollection<Play> Plays { get; set; } = new List<Play>();
        public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();

    }
}
