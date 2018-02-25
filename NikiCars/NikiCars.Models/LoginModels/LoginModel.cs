using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.LoginModels
{
    public class LoginModel
    {
        [Required]
        [StringLength(15)]
        public string LoginName { get; set; }

        public string LoginEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
