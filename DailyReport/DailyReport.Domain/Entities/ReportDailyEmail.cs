using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Shared.Enums;

namespace DailyReport.Domain.Entities
{
    public class ReportDailyEmail
    {
        public int Id { get; set; }

        public required string StoredProcedureName { get; set; }

        public required EmailContent EmailContent { get; set; }

        public bool Enabled { get; set; }

        public required ReportSchedule ReportSchedule { get; set; }

        public DateTime? LastProcessedDateTime { get; set; }

        public virtual List<ReportDailyMonitor>? ReportDailyMonitors { get; set; } 
    }

    public record EmailContent(string? EmailAddresses, string Subject, string Body, string AttachmentName);

    public record ReportSchedule(int FreqType, Weekdays FreqInterval, int FreqSubdayType, int FreqSubdayInterval, int FreqRecurrenceFactor, DateOnly ActiveStartDate, DateOnly ActiveEndDate,
        TimeSpan ActiveStartTime, TimeSpan ActiveEndTime);
}
