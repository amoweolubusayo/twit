namespace Twit.Core.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public int NumberOfFollowers { get; set; }
        public int UserId { get; set; }
    }
}