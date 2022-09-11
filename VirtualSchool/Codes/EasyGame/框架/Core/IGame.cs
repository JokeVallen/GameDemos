using EasyGame;

namespace EasyGame.Core
{
    /// <summary>
    /// 系统和数据都实现了该接口，只有实现该接口才能被加入容器
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// 初始化方法
        /// </summary>
        void Init();
    }

}