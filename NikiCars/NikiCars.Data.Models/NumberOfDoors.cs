﻿using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class NumberOfDoors : IIdentifiable
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string DoorCount { get; set; }
    }
}
