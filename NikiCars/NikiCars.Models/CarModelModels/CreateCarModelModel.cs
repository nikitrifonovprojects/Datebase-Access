﻿using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModelModels
{
    public class CreateCarModelModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int CarMakeID { get; set; }

        [Range(1, int.MaxValue)]
        public int CarTypeID { get; set; }
    }
}
