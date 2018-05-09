using System;
using Castle.DynamicProxy;
using Couchbase.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AOPForYouAndMe.Models.AOP
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly IDistributedCache _cache;

        public CacheInterceptor(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            var key = BuildCacheKey(invocation);

            var cachedValue = _cache.Get<JObject>(key);
            if (cachedValue != null)
            {
                invocation.ReturnValue = cachedValue.ToObject(invocation.Method.ReturnType);
                return;
            }

            invocation.Proceed();

            _cache.Set(key, invocation.ReturnValue,
                new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(60) });
        }

        string BuildCacheKey(IInvocation invocation)
        {
            // this isn't generic:  $"{form.Make}-{form.Model}-{form.Year}";

            var jsonBasedKey = JsonConvert.SerializeObject(invocation.Arguments) + "_DP";
            return jsonBasedKey;
        }
    }
}