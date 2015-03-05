using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using Castle.DynamicProxy;

namespace AOPForYouAndMe.Models.Caching
{
    public class CacheInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var cacheKey = BuildCacheKey(invocation);

            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                invocation.ReturnValue = HttpContext.Current.Cache[cacheKey];
                return;
            }

            invocation.Proceed();

            HttpContext.Current.Cache.Add(cacheKey, invocation.ReturnValue,
               null,
               DateTime.Now.AddSeconds(30),
               Cache.NoSlidingExpiration,
               CacheItemPriority.Normal,
               null);
        }

        string BuildCacheKey(IInvocation invocation)
        {
            var json = new JavaScriptSerializer();
            var arguments = invocation.Arguments.Select(json.Serialize);
            return string.Join("_", arguments);
        }
    }
}