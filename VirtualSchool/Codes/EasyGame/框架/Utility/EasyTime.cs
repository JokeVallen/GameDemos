using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EasyGame
{
    /// <summary>
    /// 时间系统
    /// </summary>
    public class EasyTime : AbstractSystem
    {
        TimeSystem tool;

        /// <summary>
        /// 当前时间
        /// </summary>
        public float CurTime
        {
            get => Time.timeSinceLevelLoad;
        }
        Dictionary<string, Timer> timer = new Dictionary<string, Timer>();

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            GameObject obj = new GameObject("TimeSys");
            obj.AddComponent<TimeSystem>();
            tool = obj.GetComponent<TimeSystem>();
            GameObject.DontDestroyOnLoad(obj);
        }

        #region 延迟执行
        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <param name="onFinish">执行的方法</param>
        public void Delay(float seconds, Action onFinish)
        {
            tool.Delay(seconds, onFinish);
        }

        /// <summary>
        /// 下一帧执行
        /// </summary>
        /// <param name="onFinish">执行的方法</param>
        public void Delay(Action onFinish)
        {
            tool.Delay(onFinish);
        }
        #endregion

        #region 计时器
        /// <summary>
        /// 创建一个计时器
        /// </summary>
        /// <param name="timerName">计时器名</param>
        public void CreateTheTimer(string timerName)
        {
            if (timer.ContainsKey(timerName))
            {
                Debug.LogError("已经有此计时器,创建失败");
                return;
            }

            timer.Add(timerName, new Timer(Time.timeSinceLevelLoad));
        }

        /// <summary>
        /// 获取计时器
        /// </summary>
        /// <param name="timerName">计时器名</param>
        /// <returns></returns>
        public Timer GetTheTimer(string timerName)
        {
            if (!timer.ContainsKey(timerName))
            {
                Debug.LogError("没有此计时器,获取失败");
                return null;
            }

            return timer[timerName];
        }

        /// <summary>
        /// 销毁一个计时器
        /// </summary>
        /// <param name="timerName"></param>
        /// <returns></returns>
        public bool DestoryTheTimer(string timerName)
        {
            if (!timer.ContainsKey(timerName)) return false;

            timer.Remove(timerName);
            return true;
        }
        #endregion

        class TimeSystem : MonoBehaviour
        {
            #region 方法延时
            private IEnumerator DelayCoroutine(float seconds, Action onFinish)
            {
                yield return new WaitForSeconds(seconds);
                onFinish();
            }
            private IEnumerator DelayCoroutine(Action onFinish)
            {
                yield return null;
                onFinish();
            }
            /// <summary>
            /// 延时执行
            /// </summary>
            /// <param name="seconds">秒</param>
            /// <param name="onFinish">结束后执行的方法</param>
            public void Delay(float seconds, Action onFinish)
            {
                StartCoroutine(DelayCoroutine(seconds, onFinish));
            }
            /// <summary>
            /// 延时到下一帧执行
            /// </summary>
            /// <param name="onFinish"></param>
            public void Delay(Action onFinish)
            {
                StartCoroutine(DelayCoroutine(onFinish));
            }
            #endregion
        }
    }

    #region 计时器类
    /// <summary>
    /// 计时器类
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public float StartTime { get; private set; }

        /// <summary>
        /// 经过的时间
        /// </summary>
        public float ElapsedTime
        {
            get => Time.time - StartTime;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="startTime">开始时间</param>
        public Timer(float startTime)
        {
            StartTime = startTime;
        }
    }
    #endregion
}


