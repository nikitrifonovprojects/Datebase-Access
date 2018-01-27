using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarMake :IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Country { get; set; }
    }
}
