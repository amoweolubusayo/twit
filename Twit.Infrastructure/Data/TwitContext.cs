using Microsoft.EntityFrameworkCore;
using Twit.Core.Entities;
namespace Twit.Infrastructure.Data
{
    public class TwitContext: DbContext {
        public TwitContext (DbContextOptions<TwitContext> options) : base (options) {

        }
         public DbSet<User> Users { get; set; }
         public DbSet<Post> Posts { get; set; }
         public DbSet<Follow> Follows { get; set; }
         public DbSet<PostLike> PostLikes { get; set; }

}
}


