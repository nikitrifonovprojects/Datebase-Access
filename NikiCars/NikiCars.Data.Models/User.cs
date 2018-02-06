using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class User : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string MobilePhone { get; set; }

        public City City { get; set; }

        [Required]
        public int CityID { get; set; }

        [Required]
        public int RoleID { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public string Bulstat { get; set; }

        public string Website { get; set; }

        public string PageName { get; set; }

        [Required]
        public bool IsOrganisation { get; set; }

        [Required]
        public bool IsOfficialImporter { get; set; }
    }
}
