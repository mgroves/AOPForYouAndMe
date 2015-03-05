using AOPForYouAndMe.Models.Api;

namespace AOPForYouAndMe.Models.DataLayer
{
    public interface IReportDataService
    {
        ReportResults GetData(ReportArgs args);
    }
}