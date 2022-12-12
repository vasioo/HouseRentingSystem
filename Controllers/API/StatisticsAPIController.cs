using HouseRentingSystem.Services.Statistics;
using HouseRentingSystem.Services.Statistics.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers.API
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsAPIController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsAPIController(IStatisticsService _statistics)
        {
            statistics = _statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Total();
    }
}
