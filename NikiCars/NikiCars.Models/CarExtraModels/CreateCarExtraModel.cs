using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarExtraModels
{
    public class CreateCarExtraModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
