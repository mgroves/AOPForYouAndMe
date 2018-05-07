using System.Web.Mvc;
using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Caching;
using AOPForYouAndMe.Models.Services;
using AOPForYouAndMe.Models.ViewModels;

namespace AOPForYouAndMe.Controllers
{
    public class HomeController : Controller
    {
        readonly IReportService _reportService;

        public HomeController(IReportService reportService, ICacheService cacheService)
        {
            _reportService = reportService;
            _cacheService = cacheService;  // I'm only including this dependency for illustration
                                            // in production code, this would not need to be here
        }
        private readonly ICacheService _cacheService;

        public ActionResult Index()
        {
            var model = new ReportViewModel();

            ViewBag.CacheDebug = _cacheService.GetAllCacheContents();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ReportViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var args = new ReportArgs
            {
                GroupId = model.GroupId.Value,
                AreForumsIncluded = model.AreForumsIncluded.Value
            };
            var report = _reportService.GetReportData(args);

            model.Results = report;

            ViewBag.CacheDebug = _cacheService.GetAllCacheContents();
            return View(model);
        }
    }
}
