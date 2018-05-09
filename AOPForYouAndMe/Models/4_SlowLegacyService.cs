using System;
using System.Threading;
using AOPForYouAndMe.Models.Business;

namespace AOPForYouAndMe.Models
{
    // 4 - decorator pattern
    public class SlowLegacyService : ISlowLegacyService
    {
        readonly Random _rand;

        public SlowLegacyService()
        {
            _rand = new Random();
        }

        public CarPrice GetPrice(CarPricingForm form)
        {
            Thread.Sleep(5000);
            return CarPrice.Generate();
        }
    }
}