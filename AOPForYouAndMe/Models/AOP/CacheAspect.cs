using System;
using System.Reflection;
using Couchbase.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace AOPForYouAndMe.Models.AOP
{
    [PSerializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        public static IDistributedCache Cache { private get; set; }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var key = BuildCacheKey(args);

            var cachedValue = Cache.Get<JObject>(key);
            if (cachedValue != null)
            {
                var methodInfo = (MethodInfo) args.Method;
                args.ReturnValue = cachedValue.ToObject(methodInfo.ReturnType);
                return;
            }

            args.Proceed();

            Cache.Set(key, args.ReturnValue,
                new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(60) });
        }

        string BuildCacheKey(MethodInterceptionArgs args)
        {
            // this still isn't generic:  $"{form.Make}-{form.Model}-{form.Year}";

            var jsonBasedKey = JsonConvert.SerializeObject(args.Arguments) + "_PS";
            return jsonBasedKey;
        }
    }
}