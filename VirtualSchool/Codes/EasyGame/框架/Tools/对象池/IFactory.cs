namespace EasyGame
{
    /// <summary>
    /// 工厂接口
    /// </summary>
    /// <typeparam name="T">创建的对象的类型</typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>对象</returns>
        T create();
    }
}


