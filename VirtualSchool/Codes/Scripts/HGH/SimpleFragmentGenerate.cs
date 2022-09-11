using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using System.IO;

//用于生成起始固定拼图碎片和终止固定拼图碎片
public class SimpleFragmentGenerate : ViewController
{
    [Header("必要组件")]
    [Tooltip("起始碎片和终止碎片的游戏对象")]
    [SerializeField] private GameObject[] cSimpleFragments;
    [Tooltip("精灵图层")]
    [SerializeField] private List<sSprite> cSprites = new List<sSprite>();
    private List<sSprite> cRandomSprites;
    private List<sSimplePosition> cSrList;
    private bool isRun;
    private string mPath;

    private void Start()
    {
        mPath = "Text/SimpleFragmentPosition";
    }

    private void Update()
    {
        if (!isRun)
        {
            Generation();
        }
    }

    /// <summary>
    /// 作用：是否开启固定碎片生成功能
    /// </summary>
    /// <param name="p_isRun">True为开启，False为关闭</param>
    public void SetIsRun(bool p_isRun)
    {
        isRun = p_isRun;
    }

    private void Generation()
    {
        //获取两个随机Sprite
        cRandomSprites = RandomSprite();
        //读取SimpleFragmentPosition.json文件，获取固定拼图碎片的位置列表
        ReadData();
        //设置随机选择的两个固定拼图碎片的位置
        SetPosition();
    }

    //随机获取两个Sprite
    private List<sSprite> RandomSprite()
    {
        List<sSprite> varSprites = new List<sSprite>();
        int[] indexs = { 0, 0 };
        while (indexs[0] == indexs[1])
        {
            indexs[0] = Random.Range(0, cSprites.Count);
            indexs[1] = Random.Range(0, cSprites.Count);
        }
        varSprites.Add(cSprites[indexs[0]]);
        varSprites.Add(cSprites[indexs[1]]);
        return varSprites;
    }

    //根据传入的方向参数dir从固定拼图碎片位置列表cSrList中选择相同方向的位置信息保存至列表vecs中
    //并从这个列表vecs中随机选择一个作为返回值，同时将其从拼图碎片位置列表中删除
    private Vector2 RandomPosition(string dir)
    {
        List<Vector2> vecs = new List<Vector2>();
        List<int> index = new List<int>();
        int k = 0;
        foreach (var item in cSrList)
        {
            if (item.direction.Equals(dir))
            {
                vecs.Add(new Vector2((float)item.offsetX, (float)item.offsetY));
                index.Add(k);
            }
            k++;
        }
        int i = Random.Range(0, vecs.Count);
        cSrList.RemoveAt(index[i]);
        return vecs[i];
    }

    //读取SimpleFragmentPosition.json文件，获取固定拼图碎片的位置列表
    private void ReadData()
    {
        var data = Resources.Load<TextAsset>(mPath);
        if (data != null)
        {
            sSRList datas = JsonUtility.FromJson<sSRList>(data.text);
            cSrList = datas.lists;
        }
        else
        {
            Debug.Log("HGH:读取SimpleFragmentPosition失败！！");
        }
    }

    //如果随机Sprite列表cRandomSprites和固定拼图碎片位置列表cSrList存在元素
    //则将起始固定拼图碎片和终止固定拼图碎片的Sprite替换为两个随机的Sprite
    //然后根据这两个随机Sprite中的方向信息来设置起始拼图碎片和终止拼图碎片的位置
    //设置完成后清空机Sprite列表cRandomSprites和固定拼图碎片位置列表cSrList
    private void SetPosition()
    {
        if (cRandomSprites?.Count != 0 && cSrList?.Count != 0)
        {
            sSprite v_s0 = cRandomSprites[0];
            sSprite v_s1 = cRandomSprites[1];
            ChangePosition(cSimpleFragments[0].transform, v_s0);
            ChangePosition(cSimpleFragments[1].transform, v_s1);
            SpriteRenderer sr0 = cSimpleFragments[0].GetComponent<SpriteRenderer>();
            SpriteRenderer sr1 = cSimpleFragments[1].GetComponent<SpriteRenderer>();
            sr0.sprite = v_s0.cSprite;
            sr1.sprite = v_s1.cSprite;
            isRun = true;
        }
        cRandomSprites.Clear();
        cSrList.Clear();
    }

    //根据Sprite中的方向信息获取符合条件的随机位置并设置给固定拼图碎片
    private void ChangePosition(Transform p_tf, sSprite p_sSprite)
    {
        Vector2 vec;
        switch (p_sSprite.mDirection)
        {
            case "up":
                vec = RandomPosition("up");
                break;
            case "down":
                vec = RandomPosition("down");
                break;
            case "left":
                vec = RandomPosition("left");
                break;
            case "right":
                vec = RandomPosition("right");
                break;
            default:
                vec = new Vector2(p_tf.localPosition.x, p_tf.localPosition.y);
                break;
        }
        p_tf.localPosition = new Vector3(vec.x, vec.y, p_tf.localPosition.z);
    }
}

[System.Serializable]
public class sSprite
{
    public Sprite cSprite;
    public string mDirection;
    public string color;
}

