﻿using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class Weather
    {
        [Key]
        public Guid Id { get; set; }

        
        public string Date { get; set; }

        
        public string Time { get; set; }

        
        public int T { get; set; }

        
        public double? Humidity { get; set; }

        
        public double? Td { get; set; }

        
        public int? Pressure { get; set; }

        public string? Direction { get; set; }

        public int? Velocity { get; set; }


        public int? Cloudy { get; set; }

        public int? h { get; set; }

        public int? VV { get; set; }

        public string? Event { get; set; }
    }
}
