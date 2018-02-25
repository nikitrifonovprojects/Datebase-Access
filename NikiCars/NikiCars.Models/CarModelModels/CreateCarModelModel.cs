using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class CreateCarModelModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public int CarMakeID { get; set; }

        [Required]
        public int CarTypeID { get; set; }
    }
}
