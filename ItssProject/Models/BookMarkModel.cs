using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class BookMark
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeId { get; set; }
    }
}
