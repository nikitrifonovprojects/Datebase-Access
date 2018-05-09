using System;
using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search;

namespace NikiCars.Models.CarModels
{
    public class ListShortCarModel
    {
        public int ID { get; set; }
        
        public int Kilometers { get; set; }
        
        public decimal Price { get; set; }
        
        public string Discription { get; set; }

        public DateTime DateCreated { get; set; }

        public string Title { get; set; }

        public List<Extra> Extras { get; set; }

        public Pagination Paging { get; set; }
    }
}
