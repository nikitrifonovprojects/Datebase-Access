using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikiCars.Models.CarModels
{
    public class FindCarModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }
    }
}
