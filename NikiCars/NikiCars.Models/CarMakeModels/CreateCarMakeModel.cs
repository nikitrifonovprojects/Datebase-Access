using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarMakeModels
{
    public class CreateCarMakeModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Country { get; set; }
    }
}
