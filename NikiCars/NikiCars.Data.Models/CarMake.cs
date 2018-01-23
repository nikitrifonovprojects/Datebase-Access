using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarMake
    {
        [Required]
        public int CarMakeID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
