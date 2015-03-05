using System;
using AOPForYouAndMe.Part1.Scattered.Models.Api;
using AOPForYouAndMe.Part1.Scattered.Models.Caching;
using AOPForYouAndMe.Part1.Scattered.Models.DataLayer;

namespace AOPForYouAndMe.Part1.Scattered.Models.Services
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