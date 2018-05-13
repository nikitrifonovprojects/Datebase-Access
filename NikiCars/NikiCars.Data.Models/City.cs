using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class City : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int CountyID { get; set; }
    }
}
