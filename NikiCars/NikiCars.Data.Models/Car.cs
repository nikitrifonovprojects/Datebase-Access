using System;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class Car : IIdentifiable
    {
        public int ID { get; set; }
        
        public CarModel CarModel { get; set; }

        [Required]
        public int CarModelID { get; set; }

        public CarCoupe CarCoupe { get; set; }

        [Required]
        public int CarCoupeID { get; set; }

        public NumberOfDoors NumberOfDoors { get; set; }

        [Required]
        public int NumberOfDoorsID { get; set; }

        public FuelType FuelType { get; set; }

        public int? FuelTypeID { get; set; }

        public Colour Colour { get; set; }

        public int? ColourID { get; set; }

        public GearBoxType GearboxType { get; set; }

        public int? GearboxTypeID { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public DateTime FirstRegistrationDate { get; set; }

        [Required]
        public int EngineCapacity { get; set; }

        [Required]
        public int Kilometers { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Discription { get; set; }

        [Required]
        public bool IsLeftSteering { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [Required]
        public bool IsForParts { get; set; }

        [Required]
        public bool IsDamaged { get; set; }

        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
