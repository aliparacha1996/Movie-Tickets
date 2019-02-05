using movieMVVM.Model;
using movieMVVM.ViewModel;
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

namespace movieMVVM.View
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : Window
    {
        public MovieView() 
        {
            //Intialize window
            InitializeComponent();

            // Creating new Datacontext for View Window
            MovieViewModel mvm = new MovieViewModel();

            //adding intital movie to database
            mvm.DB.AddMovie(new Movie("AAA", "a", "b"));
 
            //setting datacontext of view to the viewmodel
            DataContext = mvm;

            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

    }
}
