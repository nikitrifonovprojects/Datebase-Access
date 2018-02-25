using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarCoupeModels
{
    public class CreateCarCoupeModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public int UserID { get; set; }
    }
}
