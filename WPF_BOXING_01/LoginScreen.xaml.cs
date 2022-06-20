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
using System.Windows.Shapes;
using static WPF_BOXING_01.MoviesContext;

namespace WPF_BOXING_01
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public LoginScreen()
        {
            InitializeComponent();
            DataContext = this;  //zapisuje dane podane przez użytkownika
            using (var db = new MoviesContext())
            {

                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Title = "Jas fasola",
                        Genre = "Horror",
                        Director = "Mariusz"
                    },
                    new Movie
                    {
                        Title = "Naruto",
                        Genre = "Anime",
                        Director = "Patryk"
                    },
                    new Movie
                    {
                        Title = "Mario",
                        Genre = "Thriller",
                        Director = "Dariusz"
                    },
                    new Movie
                    {
                        Title = "Joker",
                        Genre = "Drama",
                        Director = "Mateusz"
                    },
                    new Movie
                    {
                        Title = "Leon Zawodowiec",
                        Genre = "Action",
                        Director = "Władysław"
                    },
                    new Movie
                    {
                        Title = "Interstellar",
                        Genre = "Sci-fi",
                        Director = "Marek"
                    },
                    new Movie
                    {
                        Title = "Sonic 2",
                        Genre = "Adventure",
                        Director = "Robert"
                    }

                };
                var places = new List<Place>
                {
                    new Place
                    {
                        Cinema = "Cinema City"
                    },
                    new Place
                    {
                        Cinema = "Multikino"
                    }

                };

                db.Movies.AddRange(movies);
                db.Places.AddRange(places);
                db.SaveChanges();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (UserName == null || UserPassword == null)
            {
                MessageBox.Show("Password i UserName nie moze byc puste");
            }
            else
            {

            using var db = new MoviesContext();
            var UserNames = db.UserLogins.Select(q => q.UserName).ToList(); // bierze wszystkie nazwy użytkowników z db
            var UserNameExists = UserNames.Contains(UserName); // bool sprawdza czy podana nazwa użytkownika jest w db
            var CorrectPassword = "";
            if (UserNameExists) // zapisuje poprawne hasło do correct passworda
            {
                CorrectPassword = db.UserLogins.Where(q => q.UserName == UserName).Select(q => q.Password).ToList().FirstOrDefault();
            }
            var PasswordCheck = CorrectPassword == UserPassword; // bool sprawdza czy hasło podane przez użytkownika jest przypisane do tego konta
                if (UserNameExists && PasswordCheck)
                {
                    var _ = new MainWindow();
                    _.Show();
                    Close(); // tworzy instancje main windowa i pokazuje okno
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var _ = new Registration();
            _.Show();
            Close();
        }
    }
}
