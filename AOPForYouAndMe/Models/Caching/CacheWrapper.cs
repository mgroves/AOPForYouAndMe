using System;
using System.Web;
using System.Web.Caching;

namespace AOPForYouAndMe.Models.Caching
{
    public interface ICacheWrapper
    {
        T CacheWrap<T>(Func<T> method, string cacheKey);
    }

    public class CacheWrapper : ICacheWrapper
    {
        public T CacheWrap<T>(Func<T> method, string cacheKey)
        {
            if (HttpContext.Current.Cache[cacheKey] != null)
                return (T)HttpContext.Current.Cache[cacheKey];

            var result = method();

            HttpContext.Current.Cache.Add(cacheKey, result,
               null,
               DateTime.Now.AddSeconds(30),
               Cache.NoSlidingExpiration,
               CacheItemPriority.Normal,
               null);

            return result;
        }
    }
}