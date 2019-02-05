using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace movieMVVM.Model
{
    //This class represents the Movie Model that was used in the MovieMVVM application

    public class Movie : INotifyPropertyChanged
    {
        // Next unique ID to assign to a Movie
        private static int NEXT_ID = 0;
        private const int MAX_TICKETS = 30;


        #region Properties (with getters and setters)

        // Represents number of tickets sold for this movie (Default is 0)
        private int ticketsSold;
        public int TicketsSold 
        {
            get { return ticketsSold; }
            set
            {
                ticketsSold = value;
                OnPropertyChanged("TicketsSold");
                OnPropertyChanged("TicketsRemaining");
            }
        
        }

        // Name of movie
        public String Name {get; set;}
        
        //Unique ID of Movie
        public int ID {get; set;}

        public String ImgPath {get; set;}

        public String IMDBLink {get; set;}

        private int ticketsRemaining = MAX_TICKETS;
        public int TicketsRemaining
        {
            get { return MAX_TICKETS - TicketsSold; }
        }


        #endregion //Properties (with getters and setters)


        #region Constructors
        /// <summary>
        ///Constructor for movie object.
        /// </summary>
        /// <param name = "name"> The name to give to the movie </param>
        /// <returns></returns>
        public Movie(String name, String imgPath, String imdbLink)
        {
            this.Name = name;
            this.ImgPath = imgPath;
            this.IMDBLink = imdbLink;
            this.TicketsSold = 0;
            this.ID = NEXT_ID++;
        }
        #endregion //Constructors

        #region Functions
        /// <summary>
        /// Overriden ToString function for movie object. Displays Movie Name, followed by Tickets Sold and unique ID of the movie
        /// </summary>
        /// <param></param>
        /// <returns> String with information  about the movie </returns>
        public override String ToString()
        {
            return "Name: " + Name + " Tickets Sold: " + TicketsSold + "  ID: " + ID;
        }
        #endregion // Functions

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
