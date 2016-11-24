//-----------------------------------------------------------------------
// <copyright file="CacheManager.cs" company="Digitwest.com">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: $date$
// </copyright>
// <Author>XingQuan</Author>
// <version>2.0</version>
// <summary>Web缓存数据</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Configuration;
using System.Collections;
using System.Configuration;

namespace DW.Framework.Cache
{
    public class CacheManager
    {
        private static CacheManager _instance;

        public static CacheManager Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CacheManager();
                    //Server.GetServer(typeof(CacheManager));
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public System.Web.Caching.Cache Cache
        {
            get { return System.Web.HttpContext.Current.Cache; }
        }



        protected CacheManager()
        {
           
        }

        public void Insert(string key, object value)
        {
            //绝对过期
            //Cache.Insert(key, value, null, DateTime.Now.AddSeconds(CacheDuration), TimeSpan.Zero);
            //可调整过期的项
            Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromSeconds(CacheDuration));
        }

        /// <summary>
        /// 清除带prefix前缀的缓存
        /// </summary>
        /// <param name="prefix"></param>
        public void PurgeCacheItems(string prefix)
        {
            try
            {
                if (this.Cache == null)
                    return;
                prefix = prefix.ToLower();
                List<string> itemsToRemove = new List<string>();

                IDictionaryEnumerator enumerator = this.Cache.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                        itemsToRemove.Add(enumerator.Key.ToString());
                }

                foreach (string itemToRemove in itemsToRemove)
                    this.Cache.Remove(itemToRemove);
            }
            catch (Exception)
            {
            }
            finally { }
        }

        public bool EnableCaching
        {
            get {
                bool result;
                if (bool.TryParse(ConfigurationManager.AppSettings["EnableCaching"], out result))
                    return result;
                return false;
            
            }
        }

        public int CacheDuration
        {
            get
            {
                int result;
                if (int.TryParse(ConfigurationManager.AppSettings["EnableCaching"], out result))
                    return result;
                return 300;
            }

        }
    }
}
