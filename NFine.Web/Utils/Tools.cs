using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFine.Web.Utils
{
    public class Tools
    {
        /// <summary>  
        /// 模型赋值  
        /// </summary>  
        /// <param name="target">目标</param>  
        /// <param name="source">数据源</param>  
        public static void CopyModel(object target, object source)
        {
            Type type1 = target.GetType();
            Type type2 = source.GetType();
            foreach (var mi in type2.GetProperties())
            {
                var des = type1.GetProperty(mi.Name);
                if (des != null)
                {
                    try
                    {
                        des.SetValue(target, mi.GetValue(source, null), null);
                    }
                    catch
                    { }
                }
            }
        }
    }
}