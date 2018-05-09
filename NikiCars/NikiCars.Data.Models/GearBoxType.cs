using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class GearBoxType : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Type { get; set; }
    }
}
