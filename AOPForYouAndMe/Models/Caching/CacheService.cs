using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using AOPForYouAndMe.Models.Api;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;

namespace AOPForYouAndMe.Models.Caching
{
    public interface ICacheService
    {
        bool IsCached(string key);
        void SetCacheValue(string key, object value);
        object GetCacheValue(string key);
        object GetAllCacheContents();
    }

    public class CacheService : ICacheService
    {
        private readonly IBucket _bucket;

        public CacheService()
        {
            _bucket = ClusterHelper.GetBucket("mycache");
        }

        public bool IsCached(string key)
        {
            return _bucket.Exists(key);
        }

        public void SetCacheValue(string key, object value)
        {
            _bucket.Upsert(new Document<object>
            {
                Id = key,
                Content = value,
                Expiry = 30 * 1000      // 30 seconds
            });
        }

        public object GetCacheValue(string key)
        {
            return _bucket.Get<ReportResults>(key).Value;
        }

        // this method is only for demonstration/debugging
        // purposes and is probably not necessary in production code
        public object GetAllCacheContents()
        {
            var n1ql = $"SELECT META(c).id AS cacheKey, c.* FROM `{_bucket.Name}` c";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var results = _bucket.Query<dynamic>(query);

            if (results.Rows.Any())
                return results.Rows;

            return new List<string> {"None"};
        }
    }


    // TODO: you can use this implementation instead
    // if you just want to use in-process caching
    // instead of Couchbase

//    public class CacheService : ICacheService
//    {
//        public bool IsCached(string key)
//        {
//            return HttpContext.Current.Cache[key] != null;
//        }
//
//        public void SetCacheValue(string key, object value)
//        {
//            HttpContext.Current.Cache.Add(key, value,
//                null,
//                DateTime.Now.AddSeconds(30),
//                Cache.NoSlidingExpiration,
//                CacheItemPriority.Normal,
//                null);
//        }
//
//        public object GetCacheValue(string key)
//        {
//            return HttpContext.Current.Cache[key];
//        }
//
          // this method is only for demonstration/debugging
          // purposes and is probably not necessary in production code
//        public object GetAllCacheContents()
//        {
//            var cacheDebugList = new List<string>();
//            foreach (DictionaryEntry cachedItem in HttpContext.Current.Cache)
//            {
//                var cacheRecord = cachedItem.Key + " - " + cachedItem.Value;
//                if (IsExcluded(cacheRecord))
//                    continue;
//                cacheDebugList.Add(cacheRecord);
//            }
//            if (!cacheDebugList.Any())
//                cacheDebugList.Add("None");
//            return cacheDebugList;
//        }
//
//        // exclude certain fields from being displayed that aren't relevant
//        // to demonstration
//        bool IsExcluded(string cacheRecord)
//        {
//            return
//                cacheRecord.Contains("__System.Web.WebPages.Deployment__")
//                ||
//                cacheRecord.Contains("__AppStartPage__");
//        }
//
//    }
}