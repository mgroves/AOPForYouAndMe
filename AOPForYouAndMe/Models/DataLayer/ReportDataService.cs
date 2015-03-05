using System;
using System.Threading;
using AOPForYouAndMe.Part1.Scattered.Models.Api;

namespace AOPForYouAndMe.Part1.Scattered.Models.DataLayer
{
    public class ReportDataService : IReportDataService
    {
        readonly Random _random;

        public ReportDataService()
        {
            _random = new Random();
        }

        public ReportResults GetData(ReportArgs args)
        {
            Thread.Sleep(5000);
            var results = new ReportResults();
            results.TotalViews = _random.Next(1000);
            results.TotalThreads = _random.Next(1000);
            results.TotalUsers = _random.Next(1000);
            return results;
        }
    }
}