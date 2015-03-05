using AOPForYouAndMe.Part1.Scattered.Models.Api;
using AOPForYouAndMe.Part1.Scattered.Models.DataLayer;

namespace AOPForYouAndMe.Part1.Scattered.Models.Services
{
    // using Castle DynamicProxy
    // no tangling, no scattering, yay!
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