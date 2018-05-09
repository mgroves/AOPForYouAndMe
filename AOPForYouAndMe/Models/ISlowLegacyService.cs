using AOPForYouAndMe.Models.Business;

namespace AOPForYouAndMe.Models
{
    public interface ISlowLegacyService
    {
        CarPrice GetPrice(CarPricingForm form);
    }
}