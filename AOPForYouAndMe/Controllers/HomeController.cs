using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AOPForYouAndMe.Models.Api;
using AOPForYouAndMe.Models.Services;
using AOPForYouAndMe.Models.ViewModels;

namespace AOPForYouAndMe.Controllers
{
    public class HomeController : Controller
    {
        readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            var model = new ReportViewModel();

            ViewBag.CacheDebug = GetCacheDebug();
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

            ViewBag.CacheDebug = GetCacheDebug();
            return View(model);
        }

        List<string> GetCacheDebug()
        {
            var cacheDebugList = new List<string>();
            foreach (DictionaryEntry cachedItem in HttpContext.Cache)
            {
                var cacheRecord = cachedItem.Key + " - " + cachedItem.Value;
                if (IsExcluded(cacheRecord))
                    continue;
                cacheDebugList.Add(cacheRecord);
            }
            if(!cacheDebugList.Any())
                cacheDebugList.Add("None");
            return cacheDebugList;
        }

        bool IsExcluded(string cacheRecord)
        {
            return
                cacheRecord.Contains("__System.Web.WebPages.Deployment__")
                ||
                cacheRecord.Contains("__AppStartPage__");
        }
    }
}
