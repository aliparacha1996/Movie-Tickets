using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace movieMVVM.Model
{
    //This class represents the Database (Model) that was used in the MovieMVVM application

    public class Database : INotifyPropertyChanged
    {
        // List to store movie objects. This is the list that acts as the database in this project.
        public ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        private static Database instance = null;

        private Database() { }

        public static Database ConnectToDatabase
        {
            get
            {
                if (instance == null)
                    instance = new Database();
                return instance;
            }
        }

        /// <summary>
        /// Adds a movie to the movies list.
        /// </summary>
        /// <param name="movie">The movie to add to the list.</param>
        /// <returns></returns>
        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }

        /// <summary>
        /// Returns the list of movies (WARNING: Publishes the List to an outside source, so database can be altered)
        /// </summary>
        /// <param></param>
        /// <returns>List that contains all movies created so far</returns>
        public ObservableCollection<Movie> GetMovies()
        {
            return movies;
        }


        /// <summary>
        /// Presents all movies in the database in a string format.
        /// </summary>
        /// <param></param>
        /// <returns>String that contains information about all movies in the database</returns>
        public override String ToString()
        {
            // New StringBuilder that returns that will contain the final string to return
            StringBuilder toRet = new StringBuilder(); 
            
            // Iterating through each movie present in list
            foreach (var movie in movies)
            {
                // Call ToString method of movie and append it and a new line to return string.
                toRet.Append(movie.ToString());
                toRet.Append("\n");
            }

            return toRet.ToString();
        }

        /// <summary>
        /// Increments the number of tickets sold for a particular movie.
        /// </summary>
        /// <param name = "id">ID of movie to increment tickets for</param>
        /// <returns></returns>
        public void IncrementTickets(int id)
        {
            if(id < movies.Count && id > -1)
                movies[id].TicketsSold++;
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
