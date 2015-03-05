using System;
using System.Web;
using System.Web.Caching;
using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Services;

namespace AOPForYouAndMe.Models.Caching
{
    public class ReportCacheDecorator : IReportService
    {
        readonly IReportService _target;

        public ReportCacheDecorator(IReportService reportService)
        {
            _target = reportService;
        }

        public ReportResults GetReportData(ReportArgs args)
        {
            var cacheKey = args.GroupId.ToString() + "_" + args.AreForumsIncluded.ToString();

            if (HttpContext.Current.Cache[cacheKey] != null)
                return (ReportResults)HttpContext.Current.Cache[cacheKey];

            var result = _target.GetReportData(args);

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