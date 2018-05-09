using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class ViewCarModelsByCarMakeModel
    {
        public List<int> CarMakeID { get; set; }

        public string Name { get; set; }
    }
}
