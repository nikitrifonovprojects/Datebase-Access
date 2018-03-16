using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class FindCarModelModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
