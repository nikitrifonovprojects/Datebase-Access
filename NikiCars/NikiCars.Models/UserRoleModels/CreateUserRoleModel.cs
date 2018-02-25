using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.UserRoleModels
{
    public class CreateUserRoleModel
    {
        [Required]
        [StringLength(15)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleDescription { get; set; }
    }
}
