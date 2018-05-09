using System;
using System.Collections.Generic;
using System.Linq;
using AOPForYouAndMe.Models;
using AOPForYouAndMe.Models.Business;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Mvc;

namespace AOPForYouAndMe.Controllers
{
    public class CarPriceController : Controller
    {
        private readonly ISlowLegacyService _service;

        public CarPriceController(ISlowLegacyService service, IBucketProvider bucketProvider)
        {
            _service = service;

            // this is only in the homecontroller for GetCachedItemsFromCouchbase
            // which is purely for demonstration/presentation purposes
            _bucket = bucketProvider.GetBucket("mycache");
        }
        readonly IBucket _bucket;

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CarPriceViewModel();
            model.CachedItems = GetCachedItemsFromCouchbase();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CarPricingForm form)
        {
            var model = new CarPriceViewModel();
            model.Form = form;
            if (ModelState.IsValid)
            {
                var startTime = DateTime.Now;
                var result = _service.GetPrice(form);
                model.Price = result;
                model.ElapsedTime = DateTime.Now - startTime;
                model.CachedItems = GetCachedItemsFromCouchbase();
            }
            else
            {
                model.ErrorMessage = "Invalid submission.";
            }
            return View(model);
        }

        private Dictionary<string, string> GetCachedItemsFromCouchbase()
        {
            var n1ql = $"SELECT META(c).id AS cacheKey, c AS cacheValue FROM `{_bucket.Name}` c";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = _bucket.Query<dynamic>(query);
            var cacheContentsForDisplay = new Dictionary<string, string>();
            foreach (var row in result.Rows)
                cacheContentsForDisplay.Add(row.cacheKey.ToString(), row.cacheValue.ToString());
            return cacheContentsForDisplay;
        }
    }
}