using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        // Navigation (optional but good practice)
        public Movie? Movie { get; set; }
        public Customer? Customer { get; set; }
    }

}
