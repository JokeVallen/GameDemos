using System.Collections;
using System.Collections.Generic;
using EasyGame;
using UnityEngine;

[Model("对话资源")]
public class ModDialog : AbstractModel
{
    [AutoWrited(typeof(DialogAsset))]
    public Dictionary<string, DialogAsset> DataList { get; private set; }
    public GameObject DialogPanel { get; private set; }
    public GameObject TipPanel { get; private set; }
    protected override void Init()
    {
        DialogPanel = Resources.Load<GameObject>("对话资源/对话面板");
        TipPanel = Resources.Load<GameObject>("对话资源/提示面板");
    }
}
