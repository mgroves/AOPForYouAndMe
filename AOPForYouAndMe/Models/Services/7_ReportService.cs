using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Caching;
using AOPForYouAndMe.Models.DataLayer;

namespace AOPForYouAndMe.Models.Services
{
    // using PostSharp
    // no tangling, no scattering, yay!
    public class ReportService : IReportService
    {
        readonly IReportDataService _reportDataService;

        public ReportService(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }

        [CacheAspect]
        public ReportResults GetReportData(ReportArgs args)
        {
            return _reportDataService.GetData(args);
        }
    }
}