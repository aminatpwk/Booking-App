using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booking.Application.Abstractions.Database;
using Booking.Domain.Apartments;
using Booking.Domain.Bookings;
using Booking.Domain.Owners;
using Booking.Domain.Reviews;
using Booking.Domain.Users;

namespace Booking.Infrastructure
{
    public class BookingContext : DbContext, IApplicationContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }
        public DbSet<Apartment> Apartments => Set<Apartment>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<BookingEntity> Bookings => Set<BookingEntity>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //query: per tabelen user, indexi qe eshte kolona e-mail te jete gjithmone unik
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
