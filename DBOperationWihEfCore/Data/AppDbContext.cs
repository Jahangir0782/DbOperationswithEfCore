using Microsoft.EntityFrameworkCore;

namespace DBOperationWihEfCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType() { Id = 1, Title = "INR", Description = "Indian INR" },
                new CurrencyType() { Id = 2, Title = "Dollar", Description = "Indian Dollar" },
                new CurrencyType() { Id = 3, Title = "Euro", Description = "Indian Euro" },
                new CurrencyType() { Id = 4, Title = "Dinar", Description = "Indian Dinar" }

                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Hindi", Description = "Hindi" },
                new Language() { Id = 2, Title = "Tamil", Description = "Tamil" },
                new Language() { Id = 3, Title = "Punjabi", Description = "Punjabi" },
                new Language() { Id = 4, Title = "Urdu", Description = "Urdu" }

                );
        }



        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
