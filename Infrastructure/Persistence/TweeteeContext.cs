using Microsoft.EntityFrameworkCore;
using tweetee.Application.Models;
namespace tweetee.Infrastructure.Persistence
{
    public class TweeteeContext: DbContext {
        public TweeteeContext (DbContextOptions<TweeteeContext> options) : base (options) {

        }

}
}


