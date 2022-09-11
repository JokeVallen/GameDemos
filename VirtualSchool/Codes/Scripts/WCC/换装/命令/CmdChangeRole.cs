using UnityEngine;
using EasyGame;

public class CmdChangeRole : Command
{
    public override void Excute(ViewController source, object data)
    {
        GameManager.Get<SysDressUp>().CurRole.Value = data.ToString();
    }
}
