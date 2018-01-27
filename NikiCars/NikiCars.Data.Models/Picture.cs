﻿using System.ComponentModel.DataAnnotations;

namespace NikiCars.Data.Models
{
    public class Picture : IIdentifiable
    {
        public int ID { get; set; }

        [Required]
        public byte[] PictureFile { get; set; }

        public Car Car { get; set; }

        [Required]
        public int CarID { get; set; }
    }
}
