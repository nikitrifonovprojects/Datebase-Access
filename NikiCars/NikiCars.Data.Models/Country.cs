﻿using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class Country : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
