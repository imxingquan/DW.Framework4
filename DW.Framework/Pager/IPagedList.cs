using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Data;
using System.Collections;

namespace DW.Framework.Pager
{
    public class MaxRows
    {
        /// <summary>
        /// 从配置文件取每页数据行数
        /// </summary>
        public const int ConfigPageSize = -1;
        /// <summary>
        /// 取所有行
        /// </summary>
        public const int AllRows = 0;
    }

    /// <summary>
    /// 翻页接口
    /// </summary>
    public interface IPaged
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

    /// <summary>
    /// 带翻页的List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IPaged, IList<T>
    {
        //new int Count { get; }
        IPagedList<T> Pager(int pageIndex, int pageSize, string where, CountMethod GetCountMethod);
    }
}
