using System.Threading;
using AOPForYouAndMe.Models.AOP;
using AOPForYouAndMe.Models.Business;

namespace AOPForYouAndMe.Models
{
    // 6 - PostSharp
    public class SlowLegacyService : ISlowLegacyService
    {
        [CacheAspect]
        public CarPrice GetPrice(CarPricingForm form)
        {
            Thread.Sleep(5000);
            return CarPrice.Generate();
        }
    }
}