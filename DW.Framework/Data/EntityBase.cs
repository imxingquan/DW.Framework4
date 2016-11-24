using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Data
{
    public abstract class EntityBase
    {
        protected Dictionary<string, object> _Field = new Dictionary<string, object>();
        /// <summary>
        /// 扩展字段,key 区分大小写
        /// </summary>
        public Dictionary<string, object> Field
        {
            get
            {
                if (_Field == null) throw new NullReferenceException("自定义字段容器没有被初始化");
                return _Field;
            }
            set { _Field = value; }
        }

        /// <summary>
        /// 访问扩展字段的值
        /// </summary>
        /// <param name="key">字段名称,不区分大小写</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                try
                {
                    if (_Field != null)
                    {
                        if (_Field.ContainsKey(key.ToLower()))
                            return _Field[key.ToLower()];

                        return string.Format("访问扩展字段:[{0}] 错误", key);
                    }
                    else
                    {
                        return "扩展字段集容器为空";
                    }
                }
                finally { }
            }
        }
    }
}
