using System.Net.Http.Json;
using DailyReport.Application.DTOs;
using DailyReport.Application.Services;
using DailyReport.Shared.Models;

namespace DailyReport.Client.Services
{
    public class ClientReportService(HttpClient httpClient) : IReportService
    {
        public async Task<PaginatedItems<ReportDailyItem>?> GetAllAsync(ReportFilterPaginationRequest paginationRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/reports", paginationRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PaginatedItems<ReportDailyItem>>();
        }

        public Task<ReportDailyEmailDto?> GetAsync(int id)
        {
            var uri = $"/api/reports/{id}";
            return httpClient.GetFromJsonAsync<ReportDailyEmailDto?>(uri);
        }
    }
}
