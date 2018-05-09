using System.Threading;
using AOPForYouAndMe.Models.Business;

namespace AOPForYouAndMe.Models
{
    // 5 - castle dynamicproxy
    public class SlowLegacyService : ISlowLegacyService
    {
        public CarPrice GetPrice(CarPricingForm form)
        {
            Thread.Sleep(5000);
            return CarPrice.Generate();
        }
    }
}