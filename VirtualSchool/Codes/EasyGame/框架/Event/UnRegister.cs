using System;

namespace EasyGame.Core
{
    /// <summary>
    /// 事件注销器
    /// </summary>
    /// <typeparam name="T">事件类型</typeparam>
    public class UnRegister<T> : IUnRegister
    {
        /// <summary>
        /// 事件接口
        /// </summary>
        public IEvent Event;
        /// <summary>
        /// 回调委托
        /// </summary>
        public Action<T> onEvent;
        void IUnRegister.UnRegister()
        {
            Event.UnRegister<T>(onEvent);
            Event = null;
            onEvent = null;
        }
    }

}

namespace EasyGame
{
    /// <summary>
    /// 事件注销器接口
    /// </summary>
    public interface IUnRegister
    {
        /// <summary>
        /// 注销事件
        /// </summary>
        void UnRegister();
    }
}