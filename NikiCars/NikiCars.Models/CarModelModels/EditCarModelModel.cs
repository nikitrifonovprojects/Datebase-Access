using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class EditCarModelModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public int CarMakeID { get; set; }

        [Required]
        public int CarTypeID { get; set; }
    }
}
