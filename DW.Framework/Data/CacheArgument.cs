using System;
using System.Collections.Generic;
using System.Text;

namespace DW.Framework.Data
{
    /// <summary>
    /// GetXxxs方法的委托,例如GetLists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="findExp"></param>
    /// <param name="sortExpression"></param>
    /// <param name="startRowIndex"></param>
    /// <param name="maximumRows"></param>
    /// <returns></returns>
    public delegate T ListMethod<T>(string where, string sortExpression, int startRowIndex, int maximumRows);
    /// <summary>
    /// GetXxx方法的委托，例如GetByWhere,GetCount
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="findExp"></param>
    /// <returns></returns>
    public delegate T GetMethod<T>(string where);

    public delegate int CountMethod(string where);


    public class CacheArgument<T>
    {
        public CacheArgument()
        {

        }

        /// <summary>
        /// 缓存参数构造函数
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="findExp">sql 条件</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="startRowIndex">当前页</param>
        /// <param name="maximumRows">每页行数</param>
        /// <param name="listfun">List方法委托</param>
        public CacheArgument(string key,string where,string sortExpression, int startRowIndex, int maximumRows,ListMethod<T> listMethod)
        {
            this.Key = key;

            this.Where = where;
            this.SortExpression = sortExpression;
            this.StartRowIndex = startRowIndex;
            this.MaximumRows = maximumRows;
            
            this.ListMethod = listMethod;
        }

        

        /// <summary>
        /// 缓存参数构造函数
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="findExp">sql 条件</param>
        /// <param name="isCacheing">是否开启缓存</param>
        /// <param name="cacheDuration">最后一次访问后，多少秒失效</param>
        /// <param name="getfun">Get方法委托</param>
        public CacheArgument(string key, string where, GetMethod<T> getMethod)
        {
            this.Key = key;

            this.Where = where;

            this.GetMethod = getMethod;
        }
        /// <summary>
        /// 缓存关键字
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// where语句的条件
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortExpression { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int StartRowIndex { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int MaximumRows { get; set; }
        /// <summary>
        /// GetXxxs方法委托，4个参数。例如GetLists
        /// </summary>
        public ListMethod<T> ListMethod { get; set; }
        /// <summary>
        /// Get方法,1个参数。例如:GetCount
        /// </summary>
        public GetMethod<T> GetMethod { get; set; }


        public CountMethod CoutMethod { get; set; }
    }
}
