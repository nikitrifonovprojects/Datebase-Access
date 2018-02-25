using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.UserRoleModels
{
    public class EditUserRoleModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleDescription { get; set; }
    }
}
