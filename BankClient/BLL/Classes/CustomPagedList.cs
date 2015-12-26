using System.Collections.Generic;

namespace BLL.Classes
{
    public class CustomPagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
    }
}