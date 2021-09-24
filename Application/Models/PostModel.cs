using System.ComponentModel.DataAnnotations;
namespace tweetee.Application.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}