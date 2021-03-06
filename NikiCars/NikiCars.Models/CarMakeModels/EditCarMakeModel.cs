﻿using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarMakeModels
{
    public class EditCarMakeModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Country { get; set; }
    }
}
