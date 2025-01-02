using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Shared.Models
{
    public class PaginatedItems<TEntity>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public IEnumerable<TEntity> Data { get; }
        public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrev => PageIndex > 1;

        public PaginatedItems(int pageIndex, int count, int pageSize, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
