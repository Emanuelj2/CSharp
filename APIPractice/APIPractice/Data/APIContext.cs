using APIPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPractice.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { ID = 1, Title = "The Silent Horizon", Author = "Ava Reynolds", YearPublished = 2018 },
                new Book { ID = 2, Title = "Shadows of the Mind", Author = "Liam Carter", YearPublished = 2020 },
                new Book { ID = 3, Title = "Echoes of Tomorrow", Author = "Maya Thompson", YearPublished = 2015 },
                new Book { ID = 4, Title = "The Last Ember", Author = "Noah Bennett", YearPublished = 2022 },
                new Book { ID = 5, Title = "Fragments of Light", Author = "Sophia Martinez", YearPublished = 2011 }
                );
        }
        public DbSet<Book> Books { get; set; }

    }
}
