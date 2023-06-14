using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class CoffeeShop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Gmail { get; set; }
        public int ContactNumber { get; set; }
        public string? ImageCover { get; set; }
        public double AverageRating { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
        public Boolean Service { get;set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int PostedByUser { get; set; }
        public int Approved { get; set; }
    }
}
