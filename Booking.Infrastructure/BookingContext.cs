using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Bookings;
using Booking.Domain.Owners;
using Booking.Domain.Reviews;
using Booking.Domain.Users;
using Booking.Domain.Photos;
using Booking.Domain.Apartments;
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
        public DbSet<Photo> Photos {get; set; }
        //public DbSet<Amenity> Amenities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Owner)
                .WithMany(o => o.Apartments)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.Country);
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.City);
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.Type);
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.Price);
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.LastBookedOnUtc);

            modelBuilder.Entity<BookingEntity>()
                .Property(b => b.Status)
                .HasConversion<string>();
            modelBuilder.Entity<BookingEntity>()
                .Property(b => b.ConfirmationToken)
                .HasMaxLength(64);
            modelBuilder.Entity<BookingEntity>()
                .HasIndex(b => b.ConfirmationToken)
                .IsUnique();
            modelBuilder.Entity<BookingEntity>()
                .HasIndex(b => new {b.ApartmentId, b.Start, b.End});
            modelBuilder.Entity<BookingEntity>()
                .HasIndex(b => b.Status);

            base.OnModelCreating(modelBuilder);
        }

    }
}
