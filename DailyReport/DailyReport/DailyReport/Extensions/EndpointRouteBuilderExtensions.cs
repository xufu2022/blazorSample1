using DailyReport.Application.DTOs;
using DailyReport.Application.Services;
using DailyReport.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterReportEndPoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var reportsEndpoints = endpointRouteBuilder.MapGroup("/api/reports");

        reportsEndpoints.MapPost("/list", GetListAsync)
            .WithName("reportList")
            .WithOpenApi();
        reportsEndpoints.MapGet("", GetReportByIdAsync)
            .WithName("GetDish")
            .WithOpenApi()
            .WithSummary("Get a report by providing an id.");
    }

    private static async Task<Results<NotFound, Ok<PaginatedItems<ReportDailyItem>>>> GetListAsync(ReportFilterPaginationRequest request, [FromServices] IReportService reportService)
    {
        var result = await reportService.GetAllAsync(request);
        if(result.Count==0)
            return TypedResults.NotFound();
        return TypedResults.Ok(result);
    }
    private static async Task<Results<NotFound, Ok<ReportDailyEmailDto>>> GetReportByIdAsync(int id, [FromServices] IReportService reportService)
    {
        var result = await reportService.GetAsync(id);
        if (result != null)
        {
            return TypedResults.Ok(result);
        }
        return TypedResults.NotFound();

    }
}