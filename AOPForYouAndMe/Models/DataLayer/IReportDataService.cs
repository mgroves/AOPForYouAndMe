using AOPForYouAndMe.Part1.Scattered.Models.Api;

namespace AOPForYouAndMe.Part1.Scattered.Models.DataLayer
{
    public interface IReportDataService
    {
        ReportResults GetData(ReportArgs args);
    }
}