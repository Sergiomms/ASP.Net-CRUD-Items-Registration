using Microsoft.EntityFrameworkCore;

namespace pecasAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Pecas> Pecas =>  Set<Pecas>();
    }   
}
