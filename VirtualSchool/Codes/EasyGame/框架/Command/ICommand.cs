using EasyGame.Core;

namespace EasyGame.Core
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 命令内容
        /// </summary>
        /// <param name="source">命令调用者</param>
        /// <param name="data">数据</param>
        void Excute(ViewController source, object data);
    }

    /// <summary>
    /// 命令扩展接口
    /// </summary>
    public interface ICommandExtra
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
        /// <param name="e">事件对象</param>
        void EventTrigger<T>(T e) where T : new();
    }
}

namespace EasyGame
{
    /// <summary>
    /// 命令基类
    /// </summary>
    public abstract class Command : ICommand, ICommandExtra
    {
        /// <summary>
        /// 命令本身
        /// </summary>
        protected ICommandExtra Self { get => this as ICommandExtra; }
        void ICommand.Excute(ViewController source, object data)
        {
            Excute(source, data);
        }

        /// <summary>
        /// 任务方法
        /// </summary>
        /// <param name="source">命令的发送者</param>
        /// <param name="data">数据</param>
        public abstract void Excute(ViewController source, object data);

        void ICommandExtra.EventTrigger<T>()
        {
            GameManager.Get<EasyEvent>().Trigger<T>();
        }

        void ICommandExtra.EventTrigger<T>(T e)
        {
            GameManager.Get<EasyEvent>().Trigger<T>(e);
        }
    }
}