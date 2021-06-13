using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        //Generate constructor by hovering over DataContext and right clicking. 
        //Add this configuration to our startup class to allow us to inject it into our configuration. 
        public DataContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<AppUser> Users { get; set; }
    }
}