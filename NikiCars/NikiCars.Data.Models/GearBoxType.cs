using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class GearBoxType : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Type { get; set; }
    }
}
