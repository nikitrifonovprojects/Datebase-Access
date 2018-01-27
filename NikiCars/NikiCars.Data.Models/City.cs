using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class City : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public int CountyID { get; set; }
    }
}
