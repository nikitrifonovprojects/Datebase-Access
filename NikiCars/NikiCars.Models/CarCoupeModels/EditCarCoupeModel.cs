using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarCoupeModels
{
    public class EditCarCoupeModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
