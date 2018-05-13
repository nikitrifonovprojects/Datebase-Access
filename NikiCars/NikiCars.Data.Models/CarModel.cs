using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarModel : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int CarMakeID { get; set; }

        [Range(1, int.MaxValue)]
        public int CarTypeID { get; set; }
    }
}
