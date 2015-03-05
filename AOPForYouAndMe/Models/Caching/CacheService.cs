using System;
using System.Web;
using System.Web.Caching;

namespace AOPForYouAndMe.Part1.Scattered.Models.Caching
{
    public interface ICacheService
    {
        bool IsCached(string key);
        void SetCacheValue(string key, object value);
        object GetCacheValue(string key);
    }

    public class CacheService : ICacheService
    {
        public bool IsCached(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        public void SetCacheValue(string key, object value)
        {
            HttpContext.Current.Cache.Add(key, value,
                null,
                DateTime.Now.AddSeconds(30),
                Cache.NoSlidingExpiration,
                CacheItemPriority.Normal,
                null);
        }

        public object GetCacheValue(string key)
        {
            return HttpContext.Current.Cache[key];
        }
    }
}