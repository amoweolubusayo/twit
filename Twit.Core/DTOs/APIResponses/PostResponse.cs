using Twit.Core.Entities;
namespace Twit.Core.DTOs.APIResponse
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLiked { get; set; }
        public string PostedBy { get; set; }
        public int? NumberOfLikes { get; set; }
    }
}