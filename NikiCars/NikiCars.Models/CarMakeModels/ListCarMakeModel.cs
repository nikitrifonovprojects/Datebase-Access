using NikiCars.Search;

namespace NikiCars.Models.CarMakeModels
{
    public class ListCarMakeModel
    {
        public Pagination Paging { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
