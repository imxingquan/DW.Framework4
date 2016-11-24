using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Framework.Pager
{
    public interface IPagedList
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 每页个数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        string SortExpression { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        int TotalItemCount { get; set; }
        /// <summary>
        /// 共多少页
        /// </summary>
        int TotalPageCount { get; }
    }
}
