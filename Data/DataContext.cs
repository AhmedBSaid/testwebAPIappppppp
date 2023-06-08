using Microsoft.EntityFrameworkCore;
using testwebAPIapp.Model;

namespace testwebAPIapp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Personne> Personnes { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }
    }
}
