using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarDoorModels
{
    public class EditCarDoorsModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string DoorCount { get; set; }
    }
}
