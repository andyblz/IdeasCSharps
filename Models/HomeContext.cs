using Microsoft.EntityFrameworkCore;
 
namespace BletExamIdeas.Models
{
    public class HomeContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }

        public DbSet<User> users {get;set;}
        public DbSet<Post> posts{ get;set;}

        public DbSet<Like> likes{ get;set;}
    }
}