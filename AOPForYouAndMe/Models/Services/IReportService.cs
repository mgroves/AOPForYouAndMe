using AOPForYouAndMe.Part1.Scattered.Models.Api;

namespace AOPForYouAndMe.Part1.Scattered.Models.Services
{
    public interface IReportService
    {
        ReportResults GetReportData(ReportArgs args);
    }
}