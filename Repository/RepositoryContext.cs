using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions option) 
            :base(option)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasIndex(c => new { c.MovieId, c.GenreId })
                .IsUnique(true);

            //modelBuilder.Entity<Movie>(entity =>
            //{
            //    entity.ToTable("Movies");

            //    // Setting the integer column with a check constraint

            //    //entity.Property(e => e.Rating)
            //    //    .IsRequired();

            //    // Adding a check constraint to the column
            //    entity.HasCheckConstraint("CK_Movies_Rating", $"rating >= 1 AND rating <= 5");


            //    //entity.Property(e => e.TicketPrice)
            //    //    .IsRequired();

            //    // Adding a check constraint to the column
            //    entity.HasCheckConstraint("CK_Movies_TicketPrice", $"TicketPrice >= 0 AND TicketPrice <= 100000000");
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
