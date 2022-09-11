using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDressUp
{
    public SkinnedMeshRenderer SkinnedMesh { get; set; }
    public string Part { get; set; }
    public void Excute(Transform role)
    {
        var childs = role.GetComponentsInChildren<Transform>();

        Transform hips = null;
        Transform target = null;
        foreach (var item in childs)
        {
            if (item.name == Part)
            {
                target = item;
            }

            if (item.name == "Female_Hips" || item.name == "Male_Hips")
            {
                hips = item;
            }
        }

        if (target == null) return;

        var skm = target.GetComponent<SkinnedMeshRenderer>();
        BindBones(SkinnedMesh, skm, hips.GetComponentsInChildren<Transform>());
        skm.sharedMesh = SkinnedMesh.sharedMesh;
        skm.sharedMaterials = SkinnedMesh.sharedMaterials;

    }

    void BindBones(SkinnedMeshRenderer source, SkinnedMeshRenderer target, Transform[] hips)
    {
        List<Transform> bones = new List<Transform>();

        foreach (var souceBone in source.bones)
        {
            foreach (var hip in hips)
            {
                if (souceBone.name == hip.name)
                {
                    bones.Add(hip);
                    break;
                }
            }
        }
        target.bones = bones.ToArray();
    }
}
