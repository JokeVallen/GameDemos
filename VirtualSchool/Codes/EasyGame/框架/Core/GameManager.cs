using System;
using System.Reflection;
using System.Collections.Generic;
using EasyGame;
using EasyGame.Core;
using UnityEngine;

namespace EasyGame
{
    /// <summary>
    /// 游戏管理器，外部系统访问内部系统的桥梁
    /// </summary>
    public class GameManager : EasyGameEngine<GameManager>
    {
        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void Init() { }

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void GetAllGame()
        {
            RegisterDefult();

            Assembly assembly = null;
            Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in assemblys)
            {
                if (item.GetName().Name == "Assembly-CSharp")
                {
                    assembly = item;
                }
            }

            RegisterMarkModel(assembly);
            RegisterMarkSystem(assembly);

        }

        static void RegisterMarkSystem(Assembly assembly)
        {
            var systemDir = SystemScanner.Scanning(assembly);
            foreach (var item in systemDir)
            {
                Register(item.Key, item.Value);
            }
        }

        static void RegisterMarkModel(Assembly assembly)
        {
            var modelDir = ModelScanner.Scanning(assembly);
            foreach (var item in modelDir)
            {
                Register(item.Key, item.Value);
            }
        }

        static void RegisterDefult()
        {
            Register(typeof(EasyEvent), new EasyEvent());
            Register(typeof(EasyMessage), new EasyMessage());
            Register(typeof(EasyTime), new EasyTime());
        }
    }
}