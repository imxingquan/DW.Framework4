using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Configuration;

namespace DW.Framework.Data
{
    public class DataContainer
    {
        //初始化数据服务
        private static IDataService dataService = new DataService();

        /// <summary>
        /// 返回数据服务接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>() where T : IDataProvider //参数约束：必须是IDataProvider或他的实现
        {
            try
            {
                T instance = (T)dataService.GetService(typeof(T));

                if (instance == null)
                {
                    //更具类型信息创建对象
                    instance = (T)Activator.CreateInstance(
                        Type.GetType(ConfigManager<T>.Current.GetProviderType(), true, true));
                    instance.ConnectionString = ConfigManager<T>.Current.ConnectionString;

                    dataService.AddService(typeof(T), instance);
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message+string.Format("\n尝试检查配置文件{0}对应的实例类型名称是否正确",typeof(T).Name));
            }
        }

        public static void ResetService()
        {
            dataService.ClearService();
        }
    }
}
