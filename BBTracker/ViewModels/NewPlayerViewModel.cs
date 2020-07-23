using BBTracker.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace BBTracker.ViewModels
{
    class NewPlayerViewModel :INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nick { get; set; }
        public int? Number { get; set; }
        public NewPlayerViewModel()
        {
            
        }

        public void SaveChanges()
        {
            var _context = new BBTrackerContext();

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Predicate<object> CanExecuteFunc { get; set; }
        public Predicate<object> ExecuteFunc { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteFunc(parameter);
        }
    }

}
