namespace MenOfVarnaApp.Data
{
    using MenOFVarna.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class MenOfVarnaDbContext : DbContext
    {
        public MenOfVarnaDbContext()
        {

        }

        public MenOfVarnaDbContext(DbContextOptions options) 
            : base(options) 
        { 

        }

        public virtual DbSet<Picture> Pictures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
