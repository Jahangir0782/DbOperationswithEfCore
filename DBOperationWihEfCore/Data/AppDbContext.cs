using Microsoft.EntityFrameworkCore;

namespace DBOperationWihEfCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }






    }
}
