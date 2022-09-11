using System;
using EasyGame.Core;

namespace EasyGame
{
    /// <summary>
    /// 系统层特性，添加此特性后该数据会自动添加进容器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SystemAttribute : Attribute
    {
        /// <summary>
        /// 系统类型
        /// </summary>
        public Type Type { get; private set; }

        public Type Excutor { get; private set; }
        public string ExcutorPath { get; private set; }

        public SystemAttribute() { }

        public SystemAttribute(Type excutor)
        {
            Excutor = excutor;
        }
        public SystemAttribute(Type excutor, Type type = null)
        {
            Excutor = excutor;
            Type = type;
        }

        public SystemAttribute(string excutorPath, Type excutor, Type type = null)
        {
            ExcutorPath = excutorPath;
            Excutor = excutor;
            Type = type;
        }
    }
}
