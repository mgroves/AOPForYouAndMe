using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.DataLayer;

namespace AOPForYouAndMe.Models.Services
{
    // no caching
    public class ReportService : IReportService
    {
        readonly IReportDataService _reportDataService;

        public ReportService(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }

        public ReportResults GetReportData(ReportArgs args)
        {
            return _reportDataService.GetData(args);
        }
    }
}