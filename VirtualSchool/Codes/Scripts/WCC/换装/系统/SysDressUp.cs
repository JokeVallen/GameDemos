using System.Collections;
using System.Collections.Generic;
using EasyGame;
using UnityEngine;

[System]
public class SysDressUp : AbstractSystem
{
    public BindableProperty<string> CurRole { get; set; } = new BindableProperty<string>();
    public Dictionary<string, Dictionary<string, int>> curDressUp { get; private set; } =
        new Dictionary<string, Dictionary<string, int>>();
    protected override void Init()
    {
        CurRole.Value = "girl";
        curDressUp.Add("girl", new Dictionary<string, int>());
        curDressUp.Add("boy", new Dictionary<string, int>());
        curDressUp["girl"].Add("hair", 0);
        curDressUp["girl"].Add("eyes", 0);
        curDressUp["girl"].Add("top", 0);
        curDressUp["girl"].Add("pants", 0);
        curDressUp["girl"].Add("shoes", 0);
        curDressUp["boy"].Add("hair", 0);
        curDressUp["boy"].Add("eyes", 0);
        curDressUp["boy"].Add("top", 0);
        curDressUp["boy"].Add("pants", 0);
        curDressUp["boy"].Add("shoes", 0);
    }

    public void ChangeDressUp(string part, int index)
    {
        var dressUpModel = GameManager.Get<ModDressUp>();

        var skm = dressUpModel.DressUpAsset[CurRole.Value][part][index];
        curDressUp[CurRole.Value][part] = index;
        Self.EventTrigger<EventDressUp>(new EventDressUp() { SkinnedMesh = skm, Part = part });
    }
}
