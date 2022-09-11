using System.Collections.Generic;
using System;

namespace EasyGame
{
    /// <summary>
    /// 物体对象池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class EasyPool<T> : IPool<T>
    {
        Queue<T> pool = new Queue<T>();
        /// <summary>
        /// 对象池中对象的数量
        /// </summary>
        public int Count => pool.Count;
        Func<T> method;
        Action<T> reseMethod;
        IFactory<T> factory;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="createMethod"></param>
        /// <param name="count"></param>
        /// <param name="reseMethod"></param>
        public EasyPool(Func<T> createMethod, int count, Action<T> reseMethod = null)
        {
            pool = new Queue<T>();
            method = createMethod;
            this.reseMethod = reseMethod;
            factory = new ObjectFactory<T>(method);
            for (int i = 0; i < count; i++)
            {
                T temp = factory.create();
                pool.Enqueue(temp);
            }
        }

        /// <summary>
        /// 取出
        /// </summary>
        /// <returns>对象</returns>
        public T Spawn()
        {
            return Count > 0 ? pool.Dequeue() : factory.create();
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="target">对象</param>
        public void UnSpawn(T target)
        {
            reseMethod?.Invoke(target);
            pool.Enqueue(target);
        }
    }
}


