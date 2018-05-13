using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarCoupeModels
{
    public class CreateCarCoupeModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int UserID { get; set; }
    }
}
