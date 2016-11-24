using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Framework.Pager
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int? pageIndex,
                int pageSize
            )
        {
            return ToPagedList<T>(allItems, pageIndex, pageSize, String.Empty);

        }

        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int? pageIndex,
                int pageSize,
                string sort
            )
        {
            var truePageIndex = pageIndex ?? 1;
            var itemIndex = (truePageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, truePageIndex, pageSize, totalItemCount, sort);

        }
    }
}
