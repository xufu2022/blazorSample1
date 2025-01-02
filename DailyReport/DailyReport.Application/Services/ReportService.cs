using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.DTOs;
using DailyReport.Application.Mapping;
using DailyReport.Application.Repositories;
using DailyReport.Domain.Entities;
using DailyReport.Shared.Models;

namespace DailyReport.Application.Services
{
    public interface IReportService
    {
        Task<PaginatedItems<ReportDailyItem>?> GetAllAsync(ReportFilterPaginationRequest paginationRequest);
        Task<ReportDailyEmailDto?> GetAsync(int id);
    }

    public class ReportService(IReportDailyEmailRepository reportDailyEmailRepository) : IReportService
    {
        public async Task<PaginatedItems<ReportDailyItem>?> GetAllAsync(ReportFilterPaginationRequest paginationRequest)=> await reportDailyEmailRepository.GetAllAsync(paginationRequest);

        public async Task<ReportDailyEmailDto?> GetAsync(int id)
        {
           var result= await reportDailyEmailRepository.GetAsync(id);
           return result?.ToDto();
        }

    }
}
