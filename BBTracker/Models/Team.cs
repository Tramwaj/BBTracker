using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Models
{
    class Team
    {
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}