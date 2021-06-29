//Pulls all models in this folder in.
using MonarchAPI.Models;
//Pulls all Entity stuff from here.
using Microsoft.EntityFrameworkCore;

namespace MonarchAPI.Data
{
    //We must inherit from DbContext
    public class MonarchContext : DbContext
    {
        //This is stuff.  Not sure what it does, but we need it.
        public MonarchContext(DbContextOptions<MonarchContext> options) : base(options) { }

        //Making sure to set up the Users table
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
