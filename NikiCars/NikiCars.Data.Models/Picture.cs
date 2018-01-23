namespace NikiCars.Data.Models
{
    public class Picture : IIdentifiable
    {
        public int ID { get; set; }

        public byte[] PictureFile { get; set; }

        public Car Car { get; set; }

        public int CarID { get; set; }
    }
}
