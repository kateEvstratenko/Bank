using System.Linq;
using BLL.Classes;
using DAL.Entities;

namespace BLL.Helpers
{
    public static class EnumerableHelper
    {
        public static CustomPagedList<T> ToCustomPagedList<T>(this IQueryable<T> items, int pageNumber, int pageSize) where T: IBaseEntity
        {
            var maxEndIndex = (pageNumber - 1) * pageSize + pageSize;
            var resultItems = items.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new CustomPagedList<T>()
            {
                Items = resultItems.ToList(),
                TotalItemCount = items.Count(),
                PageSize = pageSize,
                PageNumber = pageNumber,
                HasNextPage = items.Count() > maxEndIndex,
                HasPreviousPage = pageNumber > 1,
                IsFirstPage = pageNumber == 1,
                IsLastPage = items.Count() <= maxEndIndex
            };
        }

    }
}