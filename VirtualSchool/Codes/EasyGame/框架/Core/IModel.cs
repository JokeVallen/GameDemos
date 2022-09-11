using EasyGame.Core;
using System.Collections.Generic;

namespace EasyGame.Core
{
    /// <summary>
    /// 数据层接口
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        void EventTrigger<T>() where T : new();
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="e">事件实例</param>
        void EventTrigger<T>(T e) where T : new();
    }
}

namespace EasyGame
{
    /// <summary>
    /// 数据基类
    /// </summary>
    public abstract class AbstractModel : IModel, IGame
    {
        /// <summary>
        /// 数据本身
        /// </summary>
        protected IModel Self { get; private set; }

        void IModel.EventTrigger<T>()
        {
            GameManager.Get<EasyEvent>().Trigger<T>();
        }

        void IModel.EventTrigger<T>(T e)
        {
            GameManager.Get<EasyEvent>().Trigger<T>(e);
        }

        void IGame.Init()
        {
            Self = this as IModel;
            Init();
        }

        protected virtual void InitOver() { }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected virtual void Init() { }
    }
}
