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
        public DbSet<Member> Members { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<CheckIn>().ToTable("CheckIn");
        }
    }
}
