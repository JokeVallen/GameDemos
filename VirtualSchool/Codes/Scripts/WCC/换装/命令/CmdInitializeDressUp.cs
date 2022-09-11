using UnityEngine;
using EasyGame;

public class CmdInitializeDressUp : Command
{
    public override void Excute(ViewController source, object data)
    {
        var dressUpSys = GameManager.Get<SysDressUp>();
        var dressUpModel = GameManager.Get<ModDressUp>();
        foreach (var item in dressUpSys.curDressUp[dressUpSys.CurRole.Value])
        {
            var skm = dressUpModel.DressUpAsset[dressUpSys.CurRole.Value][item.Key][item.Value];
            Self.EventTrigger<EventDressUp>(new EventDressUp() { SkinnedMesh = skm, Part = item.Key });
        }

    }
}
