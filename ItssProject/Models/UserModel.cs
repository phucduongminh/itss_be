using System.ComponentModel.DataAnnotations.Schema;

namespace ItssProject.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Gmail { get; set; }
        public string? Role { get;set; }
        public string? Avatar { get; set; }
    }
}
