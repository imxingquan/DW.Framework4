/**
 * DaLian Digitwest Network Technologies Co., Ltd.(C) 2007
 * File: BizObject.cs
 * Date: 2007-9-25
 * Author: 成海涛
 * Description: 
 *      商业组件基类层
 * history:
 *	 
 */
using System;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Collections.Generic;
using DW.Framework.Utils;
using DW.Framework.Cache;
using DW.Framework.Pager;
using DW.Framework.Data;
using System.Configuration;

namespace DW.Framework.Web
{
    /// <summary>
    /// BizObject 是所有商业组件的基类
    /// </summary>
    public abstract class BizObject<T> where T:IDataProvider
    {
        private T _provider;
        //服务提供者
        protected T Provider
        {
            get { 
                //if (_provider == null)
                    _provider =  DataContainer.GetInstance<T>(); //根据类型名称获取实例
                return _provider;
            }
        }
                
        /// <summary>
        /// 转换NULL为空字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected string ConvertNullToEmptyString(string input)
        {
            return (input == null ? "" : input);
        }

        /// <summary>
        /// 清除带prefix前缀的缓存
        /// </summary>
        /// <param name="prefix"></param>
        protected void PurgeCacheItems(string prefix)
        {
            CacheManager.Current.PurgeCacheItems(prefix);
        }
              
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected U CacheData<U>(CacheArgument<U> arg)
        {
            U obj = default(U); //=null

            if (CacheManager.Current.EnableCaching
                && CacheManager.Current.Cache != null
                && CacheManager.Current.Cache[arg.Key] != null)
            {
                obj = (U)CacheManager.Current.Cache[arg.Key];
            }
            else
            {   
                if (arg.ListMethod != null) //调用GetXxxs方法
                    obj = arg.ListMethod(arg.Where, arg.SortExpression, arg.StartRowIndex, arg.MaximumRows);
                else if (arg.GetMethod != null)//调用Get方法
                    obj = arg.GetMethod(arg.Where);
                else
                    throw new ArgumentNullException("缓存参数没有获取数据的方法");
             
                if (CacheManager.Current.EnableCaching && obj != null)
                {
                    CacheManager.Current.Insert(arg.Key, obj);
                }
            }

            return obj;
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        protected U CacheData<U>(string key, U target)
        {
            U obj = default(U); //=null
            if (CacheManager.Current.EnableCaching
                && CacheManager.Current.Cache != null
                && CacheManager.Current.Cache[key] != null)
            {
                obj = (U)CacheManager.Current.Cache[key];
            }
            else
            {
                obj = target;
                if (CacheManager.Current.EnableCaching && obj != null)
                {
                    CacheManager.Current.Insert(key, obj);
                }
            }
            return obj;
        }

        public int ConfigPageSize
        {
            get
            {
                int result;
                if (int.TryParse(ConfigurationManager.AppSettings["PageSize"], out result))
                    return result;
                return 10;
            }

        }
    }
    
}
