namespace EasyGame.Core
{
    /// <summary>
    /// 容器管理类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public abstract class EasyGameEngine<T> where T : EasyGameEngine<T>, new()
    {
        private static T mEasyGame;

        static void MakeSuremEasyGame()
        {
            if (mEasyGame == null)
            {
                mEasyGame = new T();
                mEasyGame.Init();
            }
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// 容器
        /// </summary>
        protected Container mContainer = new Container();

        /// <summary>
        /// 获取系统或者数据
        /// </summary>
        /// <typeparam name="T2">类名</typeparam>
        public static T2 Get<T2>() where T2 : class
        {
            MakeSuremEasyGame();
            return mEasyGame.mContainer.Get<T2>() as T2;
        }

        /// <summary>
        /// 注册系统或者数据
        /// </summary>
        /// <param name="instance">实例</param>
        /// <typeparam name="T2">类型</typeparam>
        public void Register<T2>(T2 instance) where T2 : IGame
        {
            MakeSuremEasyGame();
            mEasyGame.mContainer.Register<T2>(instance);
        }
        /// <summary>
        /// 注册系统或者数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="instance">实例</param>
        protected static void Register(System.Type type, object instance)
        {
            MakeSuremEasyGame();
            mEasyGame.mContainer.Register(type, instance);
        }

        /// <summary>
        /// 注册系统或者数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="instance">实例</param>
        public static void RegisterModel<T2>(T2 instance) where T2 : IGame
        {
            MakeSuremEasyGame();
            mEasyGame.mContainer.Register<T2>(instance);
        }

        /// <summary>
        /// 注册数据
        /// </summary>
        /// <param name="instance">实例</param>
        /// <typeparam name="T2">类型</typeparam>
        public static bool UnRegisterModel<T2>() where T2 : IGame
        {
            MakeSuremEasyGame();
            var result = mEasyGame.mContainer.UnRegister<T2>();
            return result;
        }
    }
}


