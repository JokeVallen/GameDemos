using System;

namespace EasyGame
{
    /// <summary>
    /// 可绑定属性
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class BindableProperty<T> where T : IEquatable<T>, IComparable<T>
    {
        private T mValue = default(T);
        private T mMaxValue = default(T);
        private T mMinValue = default(T);
        private bool setMax;
        private bool setMin;

        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get => mValue;
            set
            {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    OnValueChanged?.Invoke(mValue);

                    if (setMax && value.CompareTo(mMaxValue) > 0)
                    {
                        OnValueExceed?.Invoke(mValue);
                    }
                    else if (setMin && value.CompareTo(mMinValue) < 0)
                    {
                        OnValueInferior?.Invoke(mValue);
                    }
                }
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public T MaxValue
        {
            get => mMaxValue;
            set
            {
                if (!setMax)
                {
                    setMax = true;
                }
                mMaxValue = value;
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public T MinValue
        {
            get => mMaxValue;
            set
            {
                if (!setMin)
                {
                    setMin = true;
                }
                mMinValue = value;
            }
        }

        /// <summary>
        /// 变化后要执行的方法
        /// </summary>
        public Action<T> OnValueChanged { private get; set; }

        /// <summary>
        /// 值大于最大值时触发
        /// </summary>
        public Action<T> OnValueExceed { private get; set; }

        /// <summary>
        /// 值小于最小值时触发
        /// </summary>
        public Action<T> OnValueInferior { private get; set; }
    }
}