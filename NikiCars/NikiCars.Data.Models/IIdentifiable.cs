using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikiCars.Data.Models
{
    public interface IIdentifiable
    {
        int ID { get; set; }
    }
}
