using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarCoupe : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
