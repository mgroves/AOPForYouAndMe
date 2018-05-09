using System;
using System.Threading;
using Couchbase.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace AOPForYouAndMe.Models
{
    // 3 - caching with dependency injection
    public class SlowLegacyService : ISlowLegacyService
    {
        private readonly IDistributedCache _cache;

        public SlowLegacyService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public CarPrice GetPrice(CarPricingForm form)
        {
            var key = $"{form.Make}-{form.Model}-{form.Year}";

            var cachedValue = _cache.Get<CarPrice>(key);
            if(cachedValue != null)
                return cachedValue;

            Thread.Sleep(5000);
            var result = CarPrice.Generate();

            _cache.Set(key, result,
                new DistributedCacheEntryOptions{ SlidingExpiration = TimeSpan.FromSeconds(60)});

            return result;
        }
    }
}