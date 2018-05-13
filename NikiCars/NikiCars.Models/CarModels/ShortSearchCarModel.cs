using System;
using System.Collections.Generic;
using NikiCars.Search;

namespace NikiCars.Models.CarModels
{
    public class ShortSearchCarModel
    {
        public int? CarMakeID { get; set; }

        public int? CarModelID { get; set; }

        public decimal? CarPrice { get; set; }

        public DateTime? CarYear { get; set; }

        public int? CarEngine { get; set; }

        public int? CarGearbox { get; set; }

        public bool? IsUsed { get; set; }

        public bool? IsForParts { get; set; }

        public Pagination Paging { get; set; }
    }
}
