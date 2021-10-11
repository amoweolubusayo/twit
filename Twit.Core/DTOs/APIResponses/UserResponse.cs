using Twit.Core.Entities;
namespace Twit.Core.DTOs.APIResponse
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int NumberOfPosts { get; set; }
    }
}