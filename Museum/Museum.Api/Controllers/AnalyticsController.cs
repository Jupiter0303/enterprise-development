using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Analytics;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    [HttpGet("top-exhibitions")]
    public async Task<IActionResult> GetTopExhibitions() => Ok(await analyticsService.GetTopExhibitions());

    [HttpGet("min-visitors-excursions")]
    public async Task<IActionResult> GetMinVisitorsExcursions() => Ok(await analyticsService.GetExcursionsWithMinVisitors());

    [HttpGet("theme-statistics")]
    public async Task<IActionResult> GetThemeStatistics([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate) => Ok(await analyticsService.GetThemeStatistics(startDate, endDate));

    [HttpGet("excursions-in-hall")]
    public async Task<IActionResult> GetExcursionsInHall([FromQuery] int hallNumber, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate) => Ok(await analyticsService.GetExcursionsInHall(hallNumber, startDate, endDate));

    [HttpGet("visitors-by-excursion/{excursionId}")]
    public async Task<IActionResult> GetVisitorsByExcursion(int excursionId) => Ok(await analyticsService.GetVisitorsByExcursion(excursionId));
}