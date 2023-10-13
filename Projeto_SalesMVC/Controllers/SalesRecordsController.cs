using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Services;

namespace Projeto_SalesMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService _salesRecordService)
        {
            this._salesRecordService = _salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData[nameof(minDate)] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData[nameof(maxDate)] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(minDate,maxDate);

            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData[nameof(minDate)] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData[nameof(maxDate)] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);

            return View(result);
        }
    }
}
