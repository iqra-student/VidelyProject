using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User
    {

        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        
    }

}
