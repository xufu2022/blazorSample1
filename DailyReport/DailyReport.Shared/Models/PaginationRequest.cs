using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Shared.Models
{
    public record PaginationRequest(int PageSize = 10, int PageIndex = 1);

    public record ReportFilterPaginationRequest(int? FilterIndex, bool Ascending, int PageSize = 10, int PageIndex = 1)
        : PaginationRequest(PageSize, PageIndex);
}
