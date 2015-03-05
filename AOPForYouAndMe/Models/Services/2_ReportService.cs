using System;
using System.Web;
using System.Web.Caching;
using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.DataLayer;

namespace AOPForYouAndMe.Models.Services
{
    // caching tangled and scattered
    // and direct dependency on HttpContent
    public class ReportService : IReportService
    {
        readonly IReportDataService _reportDataService;

        public ReportService(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }

        public ReportResults GetReportData(ReportArgs args)
        {
            var cacheKey = args.GroupId.ToString() + "_" + args.AreForumsIncluded.ToString();
            if(HttpContext.Current.Cache[cacheKey] != null)
                return (ReportResults)HttpContext.Current.Cache[cacheKey];

            var result = _reportDataService.GetData(args);

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