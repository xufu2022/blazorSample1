using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Shared.Enums;

namespace DailyReport.Application.DTOs
{
    public record ReportDailyItem(
        string StoredProcedureName,
        int Id,
        int? FreqType,
        Weekdays FreqInterval,
        DateOnly ActiveStartDate,
        TimeSpan ActiveStartTime,
        DateOnly ActiveEndDate,
        TimeSpan ActiveEndTime,
        DateTime? LastProcessedDateTime,
        DateTime? MonitorEndTime,
        string? Status
    );
}
