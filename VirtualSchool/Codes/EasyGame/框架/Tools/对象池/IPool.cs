namespace EasyGame
{
    /// <summary>
    /// 对象池接口
    /// </summary>
    /// <typeparam name="T">对象池对象类型</typeparam>
    public interface IPool<T>
    {
        /// <summary>
        /// 取出
        /// </summary>
        /// <returns>对象</returns>
        T Spawn();

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="target">对象</param>
        void UnSpawn(T target);
    }

}
