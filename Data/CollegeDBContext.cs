using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        // Override OnConfiguring method to specify MySQL connection string
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //      if (!optionsBuilder.IsConfigured)
    //     {
    //         optionsBuilder.UseMySql(
    //             _configuration.GetConnectionString("MySqlConnection"),
    //             new MySqlServerVersion(new Version(10, 0, 23))
    //         );
    //     }
    
    // }
    }
}