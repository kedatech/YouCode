using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCode.BE;

namespace YouCode.DAL
{
    public class ContextoDB : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Follower> Follower {  get; set; }
        public DbSet<Reaction> Reaction { get ; set; }
        public DbSet<Profile> Profile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=youcode.mssql.somee.com;Initial Catalog=YouCode;User Id=eliseodesign_SQLLogin_1;Password=qbk92kjobt;Integrated Security=False";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
