namespace NikiCars.Data.Models
{
    public class User : IIdentifiable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string MobilePhone { get; set; }

        public City City { get; set; }

        public int CityID { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public string Bulstat { get; set; }

        public string Website { get; set; }

        public string PageName { get; set; }

        public bool IsOrganisation { get; set; }

        public  bool IsOfficialImporter { get; set; }
    }
}
