using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Shared.DTOs
{
    public class PagedResponse<T>(List<T> data, int pageSize, int totalRecords, int pageNumber = 1)
    {
        public List<T> Data { get; set; } = data;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalRecords { get; set; } = totalRecords;
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
