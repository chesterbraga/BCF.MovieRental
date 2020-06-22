using BCF.MovieRental.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BCF.MovieRental.Data.Context
{
    public class MovieRentalDbContext : DbContext
    {
        public static readonly ILoggerFactory MovieRentalLoggerFactory
            = LoggerFactory.Create(builder => builder.AddConsole());

        public MovieRentalDbContext(DbContextOptions<MovieRentalDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieRentalDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseLoggerFactory(MovieRentalLoggerFactory)
               .UseInMemoryDatabase("MovieRentalDbContext");
        }
    }
}