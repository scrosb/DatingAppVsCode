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
        //many to many users to likes. 
        public DbSet<UserLike> Likes { get; set; }

        //give the entities a configuration, override dbcontext class
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //create the primary key for the userLike intermediary table.
            builder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.LikedUserId });

            //source Relationship
            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                //A source user can like many other users
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                //if we delete a user we delete the related entities, set to Deletebehavior.NoAction
                //if using SQL Server
                .OnDelete(DeleteBehavior.Cascade);

            //Other side of the relationship (liked)
            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                //A source user can like many other users
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                //if we delete a user we delete the related entities.
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}