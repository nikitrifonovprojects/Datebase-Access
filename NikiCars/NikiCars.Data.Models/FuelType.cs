using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class FuelType : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string  Name { get; set; }
    }
}
