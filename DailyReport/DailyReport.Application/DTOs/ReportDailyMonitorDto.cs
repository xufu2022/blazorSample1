using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.DTOs
{
    public class ReportDailyMonitorDto
    {
        public int Id { get; set; }

        public int ReportDailyEmailId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }

        public virtual ReportDailyEmailDto ReportDailyEmail { get; set; }
    }
}
