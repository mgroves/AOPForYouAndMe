using System;
using System.Collections.Generic;

namespace AOPForYouAndMe.Models.Business
{
    public class CarPriceViewModel
    {
        public Dictionary<string, string> CachedItems { get; set; }
        public CarPrice Price { get; set; }
        public string ErrorMessage { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public CarPricingForm Form { get; set; }
    }
}