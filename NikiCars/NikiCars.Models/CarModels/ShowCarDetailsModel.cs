using System;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModels
{
    public class ShowCarDetailsModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }
        
        public int NumberOfDoorsID { get; set; }

        public int? FuelTypeID { get; set; }

        public int? ColourID { get; set; }

        public int? GearboxTypeID { get; set; }

        public int HorsePower { get; set; }

        public DateTime FirstRegistrationDate { get; set; }

        public int EngineCapacity { get; set; }

        public int Kilometers { get; set; }

        public decimal Price { get; set; }

        public string Discription { get; set; }

        public string Title { get; set; }
    }
}
