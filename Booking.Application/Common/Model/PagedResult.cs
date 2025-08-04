using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Model
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex < TotalPages - 1;

        public PagedResult(List<T> data, int pageIndex, int pageSize, int totalCount)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        }
    }
}
