using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Caching;
using AOPForYouAndMe.Models.DataLayer;

namespace AOPForYouAndMe.Models.Services
{
    // better but still tangled and scattered
    public class ReportService : IReportService
    {
        readonly IReportDataService _reportDataService;
        readonly ICacheService _cache;

        public ReportService(IReportDataService reportDataService, ICacheService cacheService)
        {
            _reportDataService = reportDataService;
            _cache = cacheService;
        }

        public ReportResults GetReportData(ReportArgs args)
        {
            var cacheKey = args.GroupId.ToString() + "_" + args.AreForumsIncluded.ToString();
            if (_cache.IsCached(cacheKey))
                return (ReportResults) _cache.GetCacheValue(cacheKey);

            var result = _reportDataService.GetData(args);

            _cache.SetCacheValue(cacheKey, result);

            return result;
        }
    }
}