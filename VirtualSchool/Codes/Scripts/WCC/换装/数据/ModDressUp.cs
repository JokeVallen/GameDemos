using System.Collections;
using System.Collections.Generic;
using EasyGame;
using UnityEngine;

[Model("��װ��Դ")]
public class ModDressUp : AbstractModel
{
    public GameObject Girl { get; private set; }
    public GameObject Boy { get; private set; }
    public GameObject PartToggle { get; private set; }
    public Dictionary<string, Dictionary<string, List<SkinnedMeshRenderer>>> DressUpAsset { get; private set; }
        = new Dictionary<string, Dictionary<string, List<SkinnedMeshRenderer>>>();
    protected override void Init()
    {
        Girl = Resources.Load<GameObject>("��װ��Դ/FemaleModel");
        Boy = Resources.Load<GameObject>("��װ��Դ/MaleModel");
        PartToggle = Resources.Load<GameObject>("��װ��Դ/ѡ���");
        InitDressUpAsset("girl", Girl);
        InitDressUpAsset("boy", Boy);
    }

    void InitDressUpAsset(string sex, GameObject target)
    {
        DressUpAsset.Add(sex, new Dictionary<string, List<SkinnedMeshRenderer>>());
        var child = target.GetComponentsInChildren<Transform>();

        foreach (var item in child)
        {
            var str = item.name.Split('-');
            if (str.Length == 1) continue;

            var name = str[0];
            if (name == "face") continue;

            if (!DressUpAsset[sex].ContainsKey(name))
            {
                DressUpAsset[sex].Add(name, new List<SkinnedMeshRenderer>());
            }

            DressUpAsset[sex][name].Add(item.GetComponent<SkinnedMeshRenderer>());

        }
    }
}
