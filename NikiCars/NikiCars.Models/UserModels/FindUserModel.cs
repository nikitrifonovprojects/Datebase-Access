using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.UserModels
{
    public class FindUserModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public string PageName { get; set; }
    }
}
