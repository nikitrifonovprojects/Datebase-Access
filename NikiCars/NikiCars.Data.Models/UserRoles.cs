using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class UserRoles : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleDescription { get; set; }
    }
}
