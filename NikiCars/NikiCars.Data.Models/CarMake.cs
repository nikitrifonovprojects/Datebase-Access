using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class CarMake :IIdentifiable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
