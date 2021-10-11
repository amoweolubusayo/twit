using Twit.Core.Entities;
namespace Twit.Core.DTOs.APIResponse
{
    public class LikeResponse
    {
        public int PostId { get; set; }
        public int NumberOfLikes { get; set; }
    }
}