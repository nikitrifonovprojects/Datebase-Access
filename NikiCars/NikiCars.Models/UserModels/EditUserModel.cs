using System.Collections.Generic;
using NikiCars.Data.Models;

namespace NikiCars.Models.UserModels
{
    public class EditUserModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string MobilePhone { get; set; }

        public int CityID { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public string Bulstat { get; set; }

        public string Website { get; set; }

        public string PageName { get; set; }

        public List<UserRole> Roles { get; set; }

        public bool IsOrganisation { get; set; }

        public bool IsOfficialImporter { get; set; }
    }
}
