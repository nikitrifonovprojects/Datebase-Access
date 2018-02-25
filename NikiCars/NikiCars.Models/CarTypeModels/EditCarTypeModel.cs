using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarTypeModels
{
    public class EditCarTypeModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
