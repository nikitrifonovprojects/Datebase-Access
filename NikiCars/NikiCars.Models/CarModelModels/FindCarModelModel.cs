using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class FindCarModelModel
    {
        [Required]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
