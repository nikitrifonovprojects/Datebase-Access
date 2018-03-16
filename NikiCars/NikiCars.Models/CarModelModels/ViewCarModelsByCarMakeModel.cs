using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class ViewCarModelsByCarMakeModel
    {
        [Range(1, int.MaxValue)]
        public int CarMakeID { get; set; }

        public string Name { get; set; }
    }
}
