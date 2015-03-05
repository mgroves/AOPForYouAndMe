using AOPForYouAndMe.Models.Api;

namespace AOPForYouAndMe.Models.Services
{
    public interface IReportService
    {
        ReportResults GetReportData(ReportArgs args);
    }
}