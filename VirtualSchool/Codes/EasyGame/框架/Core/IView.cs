using System;
using UnityEngine;
using EasyGame.Core;


namespace EasyGame.Core
{
    /// <summary>
    /// 表现层接口
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <typeparam name="T">命令类型</typeparam>
        /// <param name="data">数据</param>
        void SendCommand<T>(object data) where T : ICommand, new();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <param name="UnRegisterWhenGameObjectDestory">是否在物体销毁后注销注册的事件</param>
        /// <returns></returns>
        IUnRegister EventRegister<T>(System.Action<T> onEvent, bool UnRegisterWhenGameObjectDestory = true) where T : new();

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
    /// 表现层基类
    /// </summary>
    public class ViewController : MonoBehaviour, IView
    {
        /// <summary>
        /// 表现层本身
        /// </summary>
        public IView Self { get => this as IView; }

        void IView.EventUnRegister<T>(Action<T> onEvent)
        {
            GameManager.Get<EasyEvent>().UnRegister<T>(onEvent);
        }

        IUnRegister IView.EventRegister<T>(Action<T> onEvent, bool UnRegisterWhenGameObjectDestory)
        {
            var Event = GameManager.Get<EasyEvent>();
            if (UnRegisterWhenGameObjectDestory)
            {
                Event.Register<T>(onEvent).UnRegisterWhenGameObjectDestory(this.gameObject);
                return null;
            }
            else
            {
                return GameManager.Get<EasyEvent>().Register<T>(onEvent);
            }
        }

        void IView.SendCommand<T>(object data)
        {
            new T().Excute(this, data);
        }
    }
}

