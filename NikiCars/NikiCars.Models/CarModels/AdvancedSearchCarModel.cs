using System;
using System.Collections.Generic;
using NikiCars.Search;

namespace NikiCars.Models.CarModels
{
    public class AdvancedSearchCarModel
    {
        public List<int> CarMake { get; set; }

        public List<int> CarModel { get; set; }

        public decimal? FromCarPrice { get; set; }

        public decimal? ToCarPrice { get; set; }

        public DateTime? FromCarYear { get; set; }

        public DateTime? ToCarYear { get; set; }

        public int? FromHorsePower { get; set; }

        public int? ToHorsePower { get; set; }

        public int? Kilometers { get; set; }

        public int? ColourID { get; set; }

        public int? CarGearbox { get; set; }

        public List<int> IncludedExtras { get; set; }

        public List<int> ExcludedExtras { get; set; }

        public bool? IsLeftSteering { get; set; }

        public bool? IsUsed { get; set; }
        
        public bool? IsForParts { get; set; }

        public CarOrderByEnum OrderBy { get; set; }

        public Pagination Paging { get; set; }
    }
}
