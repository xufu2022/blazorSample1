using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Domain.Entities
{
    public class ReportDailyMonitor
    {
        public int Id { get; set; }

        public int ReportDailyEmailId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Status { get; set; }

        public string? Message { get; set; }

        public virtual required ReportDailyEmail ReportDailyEmail { get; set; }
    }
}
