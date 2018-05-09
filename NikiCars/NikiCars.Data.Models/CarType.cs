using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarType :IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
