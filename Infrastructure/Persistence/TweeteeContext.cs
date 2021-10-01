using Microsoft.EntityFrameworkCore;
using tweetee.Application.Entities;
namespace tweetee.Infrastructure.Persistence
{
    public class TweeteeContext: DbContext {
        public TweeteeContext (DbContextOptions<TweeteeContext> options) : base (options) {

        }
         public DbSet<User> Users { get; set; }
         public DbSet<Post> Posts { get; set; }
         public DbSet<Follow> Follows { get; set; }
         public DbSet<PostLike> PostLikes { get; set; }

}
}


