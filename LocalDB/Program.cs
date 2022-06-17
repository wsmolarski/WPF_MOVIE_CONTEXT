using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
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

                };

                db.Movies.AddRange(movies);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Movies
                            orderby b.Title
                            select b;

                Console.WriteLine("All Movies in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Title);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }
    }

    public class MoviesContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Place> Places { get; set; }
    }

    public class UserLogin
    {
        [Key]
        public long Id { get; set; }
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }
        [Required()]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }

    }

    public class Reservation
    {
        [Key]
        public long ReservationId { get; set; }
        public long Id { get; set; }

        public string ReservationDate { get; set; }

        [ForeignKey("Id")]
        public UserLogin UserLogin { get; set; }

    }

    public class Movie
    {
        [Key]
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }

    }

    public class Place
    {
        [Key]
        public long PlaceId { get; set; }
        public string Cinema { get; set; }
    }

}