using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModels
{
    public class FindCarModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }
    }
}
