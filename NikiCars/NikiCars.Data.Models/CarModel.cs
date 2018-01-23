namespace NikiCars.Data.Models
{
    public class CarModel : IIdentifiable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CarMakeID { get; set; }

        public int CarTypeID { get; set; }
    }
}
