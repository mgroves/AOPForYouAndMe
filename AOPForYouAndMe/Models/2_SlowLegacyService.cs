using System.Threading;
using AOPForYouAndMe.Models.Business;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;

namespace AOPForYouAndMe.Models
{
    // 2 - naive caching
    public class SlowLegacyService : ISlowLegacyService
    {
        private readonly IBucket _bucket;

        public SlowLegacyService(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("mycache");
        }

        public CarPrice GetPrice(CarPricingForm form)
        {
            var key = $"{form.Make}-{form.Model}-{form.Year}";
            if (_bucket.Exists(key))
            {
                return _bucket.Get<CarPrice>(key).Value;
            }

            Thread.Sleep(5000);
            var result = CarPrice.Generate();

            _bucket.Upsert(new Document<CarPrice>
            {
                Id= key,
                Content = result,
                Expiry = 60 * 1000    // 60 seconds
            });

            return result;
        }
    }
}