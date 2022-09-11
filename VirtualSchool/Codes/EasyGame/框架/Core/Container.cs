using System;
using System.Collections.Generic;

namespace EasyGame.Core
{
    /// <summary>
    /// 容器
    /// </summary>
    public class Container
    {
        Dictionary<Type, IGame> mInstances = new Dictionary<Type, IGame>();

        /// <summary>
        /// 注册容器内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        public void Register<T>(T instance) where T : IGame
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
                instance.Init();
            }
        }
        /// <summary>
        /// 注册容器内容
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="instance">实例</param>
        public void Register(Type type, object instance)
        {
            var key = type;

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance as IGame;
            }
            else
            {
                mInstances.Add(key, (IGame)instance);
                ((IGame)instance).Init();
            }
        }

        /// <summary>
        /// 获取容器内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>系统或者数据</returns>
        public T Get<T>() where T : class
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                return mInstances[key] as T;
            }

            return null;
        }

        /// <summary>
        /// 注销容器内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>是否注销成功</returns>
        public bool UnRegister<T>() where T : IGame
        {
            var key = typeof(T);
            if (mInstances.ContainsKey(key))
            {
                mInstances.Remove(key);
                return true;
            }
            return false;
        }
    }
}