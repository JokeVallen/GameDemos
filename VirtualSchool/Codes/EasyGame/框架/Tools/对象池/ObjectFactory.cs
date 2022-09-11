using System;

namespace EasyGame
{
    /// <summary>
    /// 物体工厂
    /// </summary>
    /// <typeparam name="T">产品类型</typeparam>
    public class ObjectFactory<T> : IFactory<T>
    {
        Func<T> method;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="createMethod">对象创建方法</param>
        public ObjectFactory(Func<T> createMethod)
        {
            method = createMethod;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>产品</returns>
        public T create()
        {
            return method.Invoke();
        }
    }
}


