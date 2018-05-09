using System;
using AOPForYouAndMe.Models.Business;
using Couchbase.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace AOPForYouAndMe.Models.Decorator
{
    public class CachingDecorator : ISlowLegacyService
    {
        private readonly IDistributedCache _cache;
        private readonly ISlowLegacyService _inner;

        public CachingDecorator(ISlowLegacyService inner, IDistributedCache cache)
        {
            _cache = cache;
            _inner = inner;
        }

        public CarPrice GetPrice(CarPricingForm form)
        {
            var key = $"{form.Make}-{form.Model}-{form.Year}";

            var cachedValue = _cache.Get<CarPrice>(key);
            if(cachedValue != null)
                return cachedValue;

            var result = _inner.GetPrice(form);

            _cache.Set(key, result,
                new DistributedCacheEntryOptions{ SlidingExpiration = TimeSpan.FromSeconds(30)});

            return result;
        }
    }
}