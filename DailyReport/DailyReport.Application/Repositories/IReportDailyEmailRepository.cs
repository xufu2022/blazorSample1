using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.DTOs;
using DailyReport.Domain.Entities;
using DailyReport.Shared.Models;

namespace DailyReport.Application.Repositories
{
    public interface  IReportDailyEmailRepository
    {
        Task<PaginatedItems<ReportDailyItem>?> GetAllAsync(ReportFilterPaginationRequest paginationRequest);
        Task<ReportDailyEmail?> GetAsync(int id);
    }
}
