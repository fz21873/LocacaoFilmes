
using DAL.Mapping;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.Data.Context
{
    public class SqlContext:DbContext
    {
        public SqlContext(DbContextOptions<SqlContext>options):base(options) {}

        public DbSet<Director> Directors { get; set; }
        public DbSet<Film> Films { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DirectorMap());
            modelBuilder.ApplyConfiguration(new FilmMap());
        }
    }


   
 }
