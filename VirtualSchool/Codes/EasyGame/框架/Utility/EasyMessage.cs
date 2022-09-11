using System;
using System.Collections.Generic;

namespace EasyGame
{
    /// <summary>
    /// 消息系统
    /// </summary>
    public class EasyMessage : AbstractSystem
    {
        Dictionary<string, Action<object>> thismRegisteredMsgs = new Dictionary<string, Action<object>>();

        /// <summary>
        /// 监听消息
        /// </summary>
        /// <param name="messageName">消息名</param>
        /// <param name="onMsgReceived">收到后的执行方法</param>
        public void AddListener(string messageName, System.Action<object> onMsgReceived)
        {
            if (!thismRegisteredMsgs.ContainsKey(messageName))
                thismRegisteredMsgs.Add(messageName, _ => { });
            thismRegisteredMsgs[messageName] += onMsgReceived;
        }

        /// <summary>
        /// 根据消息名取消所有消息监听
        /// </summary>
        /// <param name="messageName">消息名</param>
        public void DestroyListenerAll(string messageName)
        {
            if (thismRegisteredMsgs.ContainsKey(messageName))
                thismRegisteredMsgs.Remove(messageName);
        }

        /// <summary>
        /// 根据消息的名称和收到消息后的方法取消监听的消息
        /// </summary>
        /// <param name="messageName">消息的名称</param>
        /// <param name="onMsgReceived">收到消息后执行的方法</param>
        public void DestroyListener(string messageName, System.Action<object> onMsgReceived)
        {
            if (thismRegisteredMsgs.ContainsKey(messageName))
                thismRegisteredMsgs[messageName] -= onMsgReceived;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <param name="data">要发送的数据</param>
        public void Send(string messageName, object data)
        {
            if (thismRegisteredMsgs.ContainsKey(messageName))
            {
                thismRegisteredMsgs[messageName](data);
            }
        }
    }
}


