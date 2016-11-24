using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Data;

namespace DW.Framework.Pager
{
    /// <summary>
    /// List的翻页泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>:List<T>,IPagedList<T>
    {
               
        //public PagedList(IList<T> items, int pageIndex, int pageSize, int totalItemCount, string sortExpression)
        //{
        //    this.AddRange(items);
        //    this.PageIndex = pageIndex;
        //    this.PageSize = pageSize;
        //    this.SortExpression = sortExpression;
        //    this.TotalItemCount = totalItemCount;
        //    this.TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        //}

        //属性实现
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页个数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortExpression { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalItemCount { get; set; }
        /// <summary>
        /// 共多少页
        /// </summary>
        public int TotalPageCount { get; private set; }

        
        public IPagedList<T> Pager(int pageIndex, int pageSize, string where, CountMethod GetCountMethod)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            if (GetCountMethod != null)
            {
                //调用GetCoutMethod方法计算数据总数
                this.TotalItemCount = GetCountMethod(where);
                //计算共多少页
                this.TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            }
            return this;
        }

        
    }

}
