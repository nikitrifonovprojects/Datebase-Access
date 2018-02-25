using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class ViewCarModelsByCarMakeModel
    {
        [Required]
        public int CarMakeID { get; set; }

        public string Name { get; set; }
    }
}
