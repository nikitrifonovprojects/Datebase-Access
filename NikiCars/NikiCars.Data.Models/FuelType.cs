using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class FuelType : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string  Name { get; set; }
    }
}
