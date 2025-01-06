using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewDotnetProject.Data.Config;
using NewDotnetProject.Models;

namespace NewDotnetProject.Data
{
   public class CollegeDBContext : DbContext
    {
        // private readonly IConfiguration _configuration;

    public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
    {
        // _configuration = configuration;
    }
       public DbSet<Student> Students {get; set;}
         public DbSet<College> Colleges {get; set;}

           public DbSet<User> Users {get; set;}
            public DbSet<Item> Items {get; set;}
            public DbSet<SavedItem> SavedItems { get; set; }


         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new CollegeConfig());
             modelBuilder.ApplyConfiguration(new UserConfig());
              modelBuilder.ApplyConfiguration(new ItemConfig());
              modelBuilder.ApplyConfiguration(new SavedItemConfig());
           
        }
    }
}