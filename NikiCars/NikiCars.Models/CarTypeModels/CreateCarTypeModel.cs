using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarTypeModels
{
    public class CreateCarTypeModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
