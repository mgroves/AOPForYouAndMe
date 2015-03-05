using System;
using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Caching;
using AOPForYouAndMe.Models.DataLayer;

namespace AOPForYouAndMe.Models.Services
{
    // better, still tangled and scattered
    public class ReportService : IReportService
    {
        readonly IReportDataService _reportDataService;
        readonly ICacheWrapper _cacheWrapper;

        public ReportService(IReportDataService reportDataService, ICacheWrapper cacheWrapper)
        {
            _reportDataService = reportDataService;
            _cacheWrapper = cacheWrapper;
        }

        public ReportResults GetReportData(ReportArgs args)
        {
            var cacheKey = args.GroupId.ToString() + "_" + args.AreForumsIncluded.ToString();

            Func<ReportResults> innerMethod = () =>
                _reportDataService.GetData(args);

            return _cacheWrapper.CacheWrap(innerMethod, cacheKey);
        }
    }
}