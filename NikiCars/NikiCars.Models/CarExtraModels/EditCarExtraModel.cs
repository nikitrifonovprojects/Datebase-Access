using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarExtraModels
{
    public class EditCarExtraModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
