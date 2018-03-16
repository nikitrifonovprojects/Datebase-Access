using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarTypeModels
{
    public class EditCarTypeModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
