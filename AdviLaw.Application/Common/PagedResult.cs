using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResult(List<T> items, int totalItemsCount, int pageSize, int pageNumber)
        {
            Items = items ?? new List<T>();
            TotalItemsCount = totalItemsCount;
            PageSize = pageSize > 0 ? pageSize : 1; // 🛑 تأكد إن الصفحة مش صفر
            PageNumber = pageNumber;

            TotalPages = (int)Math.Ceiling(TotalItemsCount / (double)PageSize);
            ItemsFrom = (PageNumber - 1) * PageSize + 1;
            ItemsTo = Math.Min(PageNumber * PageSize, TotalItemsCount);
        }
    }

}
