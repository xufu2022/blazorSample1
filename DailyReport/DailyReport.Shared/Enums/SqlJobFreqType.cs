using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Shared.Enums
{
    public enum FreqType
    {
        //Once = 1,                 // One-time execution
        Daily = 4,                // Recurs daily
        Weekly = 8,               // Recurs weekly
        Monthly = 16,             // Recurs monthly (specific day of the month)
        //MonthlyRelative = 32,     // Recurs monthly (specific weekday)
        //RunWhenSqlAgentStarts = 64, // Runs when SQL Agent starts
        //RunWhenCpuIdle = 128      // Runs when the CPU is idle
    }

    public enum FreqSubdayType
    {
        AtASpecificTime = 1,       // The job runs at a specific time
        //Seconds = 2,               // The job repeats every specified number of seconds
        //Minutes = 4,               // The job repeats every specified number of minutes
        Hours = 8                  // The job repeats every specified number of hours
    }
}
