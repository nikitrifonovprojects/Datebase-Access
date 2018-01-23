namespace NikiCars.Data.Models
{
    public class Picture
    {
        public int PictureID { get; set; }

        public byte[] PictureFile { get; set; }

        public Car Car { get; set; }

        public int CarID { get; set; }
    }
}
