using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarDoorModels
{
    public class CreateCarDoorModel
    {
        [Required]
        [StringLength(5)]
        public string DoorCount { get; set; }
    }
}
