using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Movie
    {
        public int id { get; set; }
        [Required]
        public  string? Name { get; set; }
        [Required]
        public string? Genre { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
