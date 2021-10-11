using System.ComponentModel.DataAnnotations;
namespace Twit.Core.DTOs.APIRequests
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}