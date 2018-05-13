using System;
using System.Collections.Generic;
using NikiCars.Search;

namespace NikiCars.Models.CarModels
{
    public class AdvancedSearchCarModel
    {
        public List<int> CarMakeIds { get; set; }

        public List<int> CarModelIds { get; set; }

        public decimal? FromCarPrice { get; set; }

        public decimal? ToCarPrice { get; set; }

        public DateTime? FromCarYear { get; set; }

        public DateTime? ToCarYear { get; set; }

        public int? FromHorsePower { get; set; }

        public int? ToHorsePower { get; set; }

        public int? Kilometers { get; set; }

        public int? ColourID { get; set; }

        public int? CarGearbox { get; set; }

        public List<int> IncludedExtrasIds { get; set; }

        public List<int> ExcludedExtrasIds { get; set; }

        public bool? IsUsed { get; set; }
        
        public bool? IsForParts { get; set; }

        public CarOrderByEnum OrderBy { get; set; }

        public Pagination Paging { get; set; }
    }
}
