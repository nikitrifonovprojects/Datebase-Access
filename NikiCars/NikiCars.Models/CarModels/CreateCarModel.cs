﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Models.CarModels
{
    public class CreateCarModel
    {
        [Required]
        public int CarModelID { get; set; }

        [Required]
        public int CarCoupeID { get; set; }

        [Required]
        public int NumberOfDoorsID { get; set; }

        public int? FuelTypeID { get; set; }

        public int? ColourID { get; set; }

        public int? GearboxTypeID { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public DateTime FirstRegistrationDate { get; set; }

        [Required]
        public int EngineCapacity { get; set; }

        [Required]
        public int Kilometers { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Discription { get; set; }

        [Required]
        public bool IsLeftSteering { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [Required]
        public bool IsForParts { get; set; }

        [Required]
        public bool IsDamaged { get; set; }

        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}