using System.Threading;
using AOPForYouAndMe.Models.Business;

namespace AOPForYouAndMe.Models
{
    // 4 - decorator pattern
    public class SlowLegacyService : ISlowLegacyService
    {
        public CarPrice GetPrice(CarPricingForm form)
        {
            Thread.Sleep(5000);
            return CarPrice.Generate();
        }
    }
}