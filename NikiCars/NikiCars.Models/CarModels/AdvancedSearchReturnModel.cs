using System;
using System.Collections.Generic;
using NikiCars.Data.Models;

namespace NikiCars.Models.CarModels
{
    public class AdvancedSearchReturnModel
    {
        public CarModel CarModel { get; set; }

        public CarMake CarMake { get; set; }

        public decimal Price { get; set; }

        public DateTime FirstRegistrationDate { get; set; }

        public int HorsePower { get; set; }

        public int Kilometers { get; set; }

        public Colour Colour { get; set; }

        public GearBoxType GearboxType { get; set; }

        public List<Extra> Extras { get; set; }

        public bool IsLeftSteering { get; set; }

        public bool IsUsed { get; set; }

        public bool IsForParts { get; set; }
    }
}
