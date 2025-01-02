using DailyReport.Application.DTOs;
using DailyReport.Application.Services;
using DailyReport.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet(Name = "GetList")]
        public async Task<PaginatedItems<ReportDailyItem>?> GetList()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //    {
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //        TemperatureC = Random.Shared.Next(-20, 55),
            //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //    })
            //    .ToArray();
            var  paginationRequest = new ReportFilterPaginationRequest(1,false,4,0);
           var result=await _reportService.GetAllAsync(paginationRequest);
            return result;
        }
    }
}
