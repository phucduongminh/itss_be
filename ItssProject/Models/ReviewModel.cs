using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeId { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }
        public string? ReviewAt { get; set; }
        public string? EditAt { get; set; }
    }
}
