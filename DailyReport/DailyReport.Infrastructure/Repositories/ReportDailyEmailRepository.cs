using System.Linq;
using DailyReport.Application.DTOs;
using DailyReport.Application.Repositories;
using DailyReport.Domain.Entities;
using DailyReport.Infrastructure.Persistence;
using DailyReport.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DailyReport.Infrastructure.Repositories;

public class ReportDailyEmailRepository(ReportDailyDbContext context) : IReportDailyEmailRepository
{

    public async Task<ReportDailyEmail?> GetAsync(int id)
    {
        return await context.ReportDailyEmails
            .Include(rde => rde.ReportDailyMonitors)
            .FirstOrDefaultAsync(rde => rde.Id == id);
    }
    public async Task<PaginatedItems<ReportDailyItem>?> GetAllAsync(ReportFilterPaginationRequest paginationRequest)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex-1;

        //var dailyMonitorWithRowIndex = context.ReportDailyMonitors
        //    .GroupBy(rdm => rdm.ReportDailyEmailId)
        //    .SelectMany(g => g
        //        .OrderByDescending(rdm => rdm.Id)
        //        .Select((rdm, index) => new
        //        {
        //            rdm.ReportDailyEmailId,
        //            rdm.Status,
        //            RowIndex = index + 1 // RowIndex starts from 1
        //        })
        //    );
        //var query = from rde in context.ReportDailyEmails
        //            join x in dailyMonitorWithRowIndex
        //                on rde.Id equals x.ReportDailyEmailId
        //            where x.RowIndex == 1
        //            orderby rde.StoredProcedureName
        //            select new ReportDailyItem
        //            (
        //                rde.StoredProcedureName,
        //                rde.Id,
        //                        rde.ReportSchedule.FreqType,
        //                        rde.ReportSchedule.FreqInterval,
        //                        rde.ReportSchedule.ActiveStartDate,
        //                        rde.ReportSchedule.ActiveStartTime,
        //                        rde.ReportSchedule.ActiveEndDate,
        //                        rde.ReportSchedule.ActiveEndTime,
        //                        rde.LastProcessedDateTime,
        //                        rde.LastProcessedDateTime,
        //                        x != null ? x.Status : null);
        var query = context.ReportDailyEmails.GroupJoin(
                context.ReportDailyMonitors.OrderByDescending(m => m.Id),
                email => email.Id,
                monitor => monitor.ReportDailyEmailId,
                (email, monitors) => new { Email = email, LatestMonitor = monitors.FirstOrDefault() }
            ).OrderBy(r => r.Email.StoredProcedureName).Select(reportEmail => new ReportDailyItem
            (
                reportEmail.Email.StoredProcedureName,
                reportEmail.Email.Id,
                reportEmail.Email.ReportSchedule.FreqType,
                reportEmail.Email.ReportSchedule.FreqInterval,
                reportEmail.Email.ReportSchedule.ActiveStartDate,
                reportEmail.Email.ReportSchedule.ActiveStartTime,
                reportEmail.Email.ReportSchedule.ActiveEndDate,
                reportEmail.Email.ReportSchedule.ActiveEndTime,
                reportEmail.Email.LastProcessedDateTime,
                reportEmail.LatestMonitor != null ? reportEmail.LatestMonitor.EndTime : null,
                reportEmail.LatestMonitor != null ? reportEmail.LatestMonitor.Status : null
            ));//.OrderBy(reportDailyItem => reportDailyItem.StoredProcedureName);

        var countAsync = await query.CountAsync();

        //query = query.OrderBy(r => r.Id);
        //if (paginationRequest.FilterIndex != null)
        //{
        //    query = paginationRequest.FilterIndex switch
        //    {
        //        1 => paginationRequest.Ascending ? query.OrderBy(r => r.StoredProcedureName) : query.OrderByDescending(r => r.StoredProcedureName),
        //        2 => paginationRequest.Ascending ? query.OrderBy(r => r.MonitorEndTime) : query.OrderByDescending(r => r.MonitorEndTime),
        //        3 => paginationRequest.Ascending ? query.OrderBy(r => r.Status) : query.OrderByDescending(r => r.Status),
        //        _ => query.OrderBy(r => r.Id) 
        //    };
        //}
        //else
        //{
        //    query = query.OrderBy(r => r.Id);
        //}


        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();


        return new PaginatedItems<ReportDailyItem>(pageIndex + 1, countAsync,pageSize,  items);
    }
}