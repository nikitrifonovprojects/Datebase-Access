using System;

namespace NikiCars.Data.Models
{
    public class Car
    {
        public int CarID { get; set; }

        public CarModel CarModel { get; set; }

        public int CarModelID { get; set; }

        public CarCoupe CarCoupe { get; set; }

        public int CarCoupeID { get; set; }

        public NumberOfDoors NumberOfDoors { get; set; }

        public int NumberOfDoorsID { get; set; }

        public FuelType FuelType { get; set; }

        public int FuelTypeID { get; set; }

        public Colour Colour { get; set; }

        public int ColourID { get; set; }

        public GearBoxType GearboxType { get; set; }

        public int GearboxTypeID { get; set; }

        public int HorsePower { get; set; }

        public DateTime FirstRegistrationDate { get; set; }

        public int EngineCapacity { get; set; }

        public int Kilometers { get; set; }

        public decimal Price { get; set; }

        public string Discription { get; set; }

        public bool IsLeftSteering { get; set; }

        public bool IsUsed { get; set; }

        public bool IsForParts { get; set; }

        public bool IsDamaged { get; set; }

        public string Title { get; set; }

        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        public int UserID { get; set; }
    }
}
