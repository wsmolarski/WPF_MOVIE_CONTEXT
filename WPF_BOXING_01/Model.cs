using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_BOXING_01
{

    public class MoviesContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Place> Places { get; set; }

        public MoviesContext():base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Program.MoviesContext;Integrated Security=True")
        {
            
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
}
