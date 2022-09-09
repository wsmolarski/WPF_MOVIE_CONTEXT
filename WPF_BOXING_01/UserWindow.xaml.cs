using System;
using System.Data.SqlClient;
using System.Configuration;
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
using static WPF_BOXING_01.MoviesContext;
using System.Data;

namespace WPF_BOXING_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        
        public UserWindow()
        { 
            InitializeComponent();
            using (var db = new MoviesContext())
            {
                var e = db.Movies
                    .Select(q => q)
                    .ToList();
                Reservations.ItemsSource = e;
            }
        }




        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            var row = (Movie)Reservations.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("No movie is selected");
            }
            else
            if (row.AvailablePlaces == 0)
            {
                MessageBox.Show("All places for this movie are finished");
            }
            else
            {
                using (var db = new MoviesContext()) // po wybraniu filmu rezerwuje jedno z dostępnych miejsc
                {
                    var selectedMovie = db.Movies
                        .Where(t => t.MovieId == row.MovieId)
                        .ToList()
                        .LastOrDefault();
                    
                    selectedMovie.AvailablePlaces --;
                    db.SaveChanges();
                }
                Update();
            }
        }
        private void Update()
        {
            var _ = new UserWindow();
            _.Show();
            Close();
        }
    }
}