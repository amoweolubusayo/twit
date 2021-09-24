namespace tweetee.Application.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
        public bool IsDeleted { get; set; } 
        
    }
}