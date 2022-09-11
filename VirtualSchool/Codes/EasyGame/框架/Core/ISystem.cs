using System;
using EasyGame.Core;
using EasyGame;

namespace EasyGame.Core
{
    /// <summary>
    /// 系统接口
    /// </summary>
    public interface ISystem
    {
        IExcutor Excutor { set; get; }

        /// <summary>
        /// 初始化完成
        /// </summary>
        void InitOver();

        /// <summary>
        /// 每帧执行
        /// </summary>
        void Excute();
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

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <returns>事件注销器接口</returns>
        IUnRegister EventRegister<T>(System.Action<T> onEvent) where T : new();

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        void EventUnRegister<T>(System.Action<T> onEvent);
    }
}

namespace EasyGame
{
    /// <summary>
    /// 系统基类
    /// </summary>
    public abstract class AbstractSystem : ISystem, IGame
    {
        /// <summary>
        /// 系统本身
        /// </summary>
        protected ISystem Self { get; private set; }
        IExcutor ISystem.Excutor { set; get; }

        IUnRegister ISystem.EventRegister<T>(Action<T> onEvent)
        {
            return GameManager.Get<EasyEvent>().Register<T>(onEvent);
        }

        void ISystem.EventTrigger<T>()
        {
            GameManager.Get<EasyEvent>().Trigger<T>();
        }

        void ISystem.EventTrigger<T>(T e)
        {
            GameManager.Get<EasyEvent>().Trigger<T>(e);
        }

        void ISystem.EventUnRegister<T>(Action<T> onEvent)
        {
            GameManager.Get<EasyEvent>().UnRegister<T>(onEvent);
        }

        void IGame.Init()
        {
            Self = this as ISystem;
            Init();
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected virtual void Init() { }

        void ISystem.Excute()
        {
            Excute();
        }

        void ISystem.InitOver()
        {
            InitOver();
        }

        protected virtual void InitOver() { }

        /// <summary>
        /// 每帧执行
        /// </summary>
        protected virtual void Excute() { }

        protected T GetExcutor<T>() where T : Excutor
        {
            return Self.Excutor as T;
        }
    }

}
