using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using PostSharp.Aspects;

namespace AOPForYouAndMe.Part1.Scattered.Models.Caching
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var cacheKey = BuildCacheKey(args);

            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                args.ReturnValue = HttpContext.Current.Cache[cacheKey];
                return;
            }

            args.Proceed();

            HttpContext.Current.Cache.Add(cacheKey, args.ReturnValue,
               null,
               DateTime.Now.AddSeconds(30),
               Cache.NoSlidingExpiration,
               CacheItemPriority.Normal,
               null);
        }

        string BuildCacheKey(MethodInterceptionArgs args)
        {
            var json = new JavaScriptSerializer();
            var arguments = args.Arguments.Select(json.Serialize);
            return string.Join("_", arguments);
        }
    }
}