using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class CoffeeCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int CoffeeShopId { get; set; }
        public int Category { get; set; }
    }
}
