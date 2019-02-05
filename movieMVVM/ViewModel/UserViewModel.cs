using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using movieMVVM.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Windows.Input;

namespace movieMVVM.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {

        // Command to execute when a new movie is added
        public ICommand BuyTicketCommand { get; set; }

        Database db = Database.ConnectToDatabase;
        public ObservableCollection<Movie> DBMovies
        {
            get
            {
                return db.GetMovies();
            }
        }
        private void buyTicket(object id)
        {
            db.IncrementTickets((int)id);
            Messenger.Default.Send<String>("DB");
        }

        public UserViewModel()
        {
            BuyTicketCommand = new RelayCommand(buyTicket);
        }



        // PropertyChangedEventHandler to notify view that value for the property in question has changed. 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
      
    }


}
