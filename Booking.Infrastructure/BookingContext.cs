using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Apartments;
using Booking.Domain.Bookings;
using Booking.Domain.Owners;
using Booking.Domain.Reviews;
using Booking.Domain.Users;

namespace Booking.Infrastructure
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //query: per tabelen user, indexi qe eshte kolona e-mail te jete gjithmone unik
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
