namespace NikiCars.Data.Models
{
    public class City : IIdentifiable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountyID { get; set; }
    }
}
