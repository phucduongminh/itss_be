using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class ReviewReaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public bool like { get; set; }
        public bool DisLike { get; set; }
        public string LikedAt { get; set; }
        public string DisLikedAt { get; set; }
    }
}
