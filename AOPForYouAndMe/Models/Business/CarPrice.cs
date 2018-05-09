using System;

namespace AOPForYouAndMe.Models.Business
{
    public class CarPrice
    {
        public decimal Fair { get; set; }
        public decimal Good { get; set; }
        public decimal Excellent { get; set; }

        // convenience method for creating a car price
        public static CarPrice Generate()
        {
            var rand = new Random();
            var result = new CarPrice();
            result.Fair = rand.Next(300000, 3000000) / 100.0M;
            result.Good = result.Fair * 1.1M;
            result.Excellent = result.Good * 1.1M;
            return result;
        }
    }
}