using System;
using System.ComponentModel.DataAnnotations;
using NikiCars.Data.Models;

namespace NikiCars.Models.CarModels
{
    public class EditCarModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        public CarModel CarModel { get; set; }
        
        [Range(1, int.MaxValue)]
        public int CarModelID { get; set; }

        [Range(1, int.MaxValue)]
        public int CarCoupeID { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfDoorsID { get; set; }

        public int? FuelTypeID { get; set; }

        public int? ColourID { get; set; }

        public int? GearboxTypeID { get; set; }

        [Range(1, int.MaxValue)]
        public int HorsePower { get; set; }

        [Required]
        public DateTime FirstRegistrationDate { get; set; }

        [Range(1, int.MaxValue)]
        public int EngineCapacity { get; set; }

        [Range(1, int.MaxValue)]
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
    }
}
