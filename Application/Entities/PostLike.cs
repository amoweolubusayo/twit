namespace tweetee.Application.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public int NumberOfLikes { get; set; }
        public int PostId { get; set; }

    }
}