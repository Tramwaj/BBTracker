using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBTracker.Models
{
    public class PlayerGame
    {              
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }        
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public bool TeamB { get; set; }
    }
}
