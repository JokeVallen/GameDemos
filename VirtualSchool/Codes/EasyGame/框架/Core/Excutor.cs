using UnityEngine;
using EasyGame.Core;

namespace EasyGame
{
    public class Excutor : MonoBehaviour, IExcutor
    {
        ISystem IExcutor.System { get; set; }
        private IExcutor self;

        private void Awake()
        {
            self = this as IExcutor;
            Init();
        }

        private void Start()
        {
            self.System.InitOver();
        }

        void Update()
        {
            Excute();
            self.System?.Excute();
        }

        protected virtual void Init() { }
        protected virtual void Excute() { }
    }


}

namespace EasyGame.Core
{
    public interface IExcutor
    {
        ISystem System { get; set; }
    }
}