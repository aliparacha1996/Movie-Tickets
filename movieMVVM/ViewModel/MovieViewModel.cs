using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using movieMVVM.Model;
using movieMVVM.ViewModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Messaging;

namespace movieMVVM.ViewModel
{
    // This class is the main view model for MovieMVVM application. It inherits from INotifyPropertyChanged so it can update the View as entries change. 
    public class MovieViewModel : INotifyPropertyChanged
    {

        #region fields

        // PropertyChangedEventHandler to notify view that value for the property in question has changed. 
        public event PropertyChangedEventHandler PropertyChanged;

        // Match only digits
        Regex regex = new Regex(@"^[\d]+$");

        String Path = @"C:\Users\aparacha\Documents\Visual Studio 2013\Projects\movieMVVM\movieMVVM\View\";


        #endregion //fields

        #region Properties

        // Property that defines the Database this viewmodel is connected to.
        private Database db = Database.ConnectToDatabase;
        public Database DB
        {
            get { return db; }
            set
            {
                if (value != null)
                {
                    db = value;
                }
            }
        }

        private Movie latestMovie = null;
        public Movie LatestMovie
        {
            get { return db.GetMovies()[db.GetMovies().Count - 1]; }

            set
            {
                if (value != null)
                {
                    latestMovie = value;
                    OnPropertyChanged("LatestMovie");
                }
            }
        }


        #region ICommand Interface

        // Command to execute when a new movie is added
        public ICommand AddMovieCommand { get; set; }

        // Command to execute when ticket is sold for a particular movie
        public ICommand SellTicketCommand { get; set; }

        #endregion //ICommand

        // The new ID entered
        private string  idText = "";
        public string IdText
        {
            get
            {
                return idText;
            }
            set
            {
                if (idText != value)
                {
                    idText = value;
                    changeValidID(idText);
                }
            }
        }
        
        // Message to display when new Id entered is invalid or valid. (refer to changeValidID function)
        private string validateID = "Please enter an ID.";
        public string ValidateID { 
            get 
            { 
                return validateID; 
            }
            set
            {
                if (validateID != value)
                {
                    validateID = value;
                    OnPropertyChanged("ValidateID");
                }
            }
        }

        #endregion //Properties

        #region Constructors

        // Intializes ICommands to execute
        public MovieViewModel()
        {
            AddMovieCommand = new RelayCommand(addMovie, canAddMovie);
            SellTicketCommand = new RelayCommand(sellTicket, canSellTicket);
            Messenger.Default.Register<String>( this, (action) => UpdateDatabase(action));
        }

        private object UpdateDatabase(string action)
        {
            OnPropertyChanged("DB");
            return null;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Raises Property Changed Notification
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        /// <returns></returns>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Adds new movie to database.
        /// </summary>
        /// <param name="name">Name of Movie</param>
        /// <returns></returns>
        private void addMovie(object name)
        {
            var values = (object[]) name;
            db.AddMovie(new Movie(values[0].ToString(), Path + values[1].ToString(), values[2].ToString()));

            // Notify View that database has been changed
            OnPropertyChanged("DB");
            changeValidID(idText);
            OnPropertyChanged("LatestMovie");

        }

        /// <summary>
        /// Checks if Movie can be added to database.
        /// </summary>
        /// <param name="name"> Movie name </param>
        /// <returns>Return true if name is not null or empty, false otherwise</returns>
        private bool canAddMovie(object name)
        {
            if (name == null) return false;
            var values = (object[]) name;

            Debug.Print(values[0].ToString());
            if (String.IsNullOrEmpty(values[0].ToString()) || String.IsNullOrEmpty(values[1].ToString()) || String.IsNullOrEmpty(values[2].ToString()))
                return false;
            else return true;
        }


        /// <summary>
        /// Sells ticket for movie and updates database
        /// </summary>
        /// <param name="id"> Movie id </param>
        /// <returns></returns>
        private void sellTicket(object id)
        {
            db.IncrementTickets(int.Parse((string)id));
            // Notify view that database has been updated
            OnPropertyChanged("DB");
        }

        /// <summary>
        /// Checks if ticket for Movie can be sold.
        /// </summary>
        /// <param name="id"> Movie id </param>
        /// <returns>Return false is id null, empty, Not a Number or outside of valid range, true otherwise</returns>
        private bool canSellTicket(object id)
        {
            // if id is null, empty or does not match regex defined above return false
            if (String.IsNullOrEmpty(id.ToString()) || !regex.IsMatch(id.ToString())) return false;
            else
            {
                // Convert to integer
                int ticketId = int.Parse((string)id);
                //Checks if id is within valid range
                if (ticketId < 0 || ticketId >= db.GetMovies().Count)
                    return false;
                else return true;
            }
        }


        /// <summary>
        /// Changes ValidID message, depending on whether id is valid (not null, empty, not a number or outside of id range) or not.
        /// </summary>
        /// <param name="idText"> ID for movie </param>
        /// <returns></returns>
        private void changeValidID(String idText)
        {
            // if id is null, empty or not a digits return false
            if (String.IsNullOrEmpty(idText.ToString()) || !regex.IsMatch(idText.ToString())) return;

            //Parse id to integer
            int ticketId = int.Parse((string)idText);

            // If not within valid range change message to invalid
            if (ticketId < 0 || ticketId >= db.GetMovies().Count)
            {
                ValidateID = "Please enter ID within valid range. Valid Range is 0 to " + (db.GetMovies().Count - 1) + " inclusive";
            }
            // else change to valid message
            else ValidateID = "The ID you are entering is: " + ticketId;
        }

    }
        #endregion //Functions


    //This class helps in the initialization of ICommands. It is a generic class that takes in parameters of type object.
    public class RelayCommand : ICommand
    {
        # region fields

        private Action<object> execute;
        private Predicate<object> canExecute;
        private event EventHandler CanExecuteChangedInternal;
        
        #endregion //Fields

        #region Constructors
        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion //Constructors

        #region functions
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        #endregion //functions
    }

        public class MovieFields : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
