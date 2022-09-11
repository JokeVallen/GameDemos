using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using System.IO;

//用来生成固定拼图碎片的位置信息并保存至SimpleFragmentPosition.json文件中
public class SimpleReferenceRule : ViewController
{
    private List<sSimplePosition> mSrList = new List<sSimplePosition>();
    private string mPath;
    private float mHeight = 0.83f;
    private float mWidth = 0.84f;
    private float mDistance = 1.11f;

    private void Start()
    {
        mPath = Application.dataPath + "/HGH/Datas/SimpleFragmentPosition.json";
        //SavePosition();
    }

    //根据拼图碎片放置区域从左至右从上至下的原则对拼图碎片进行排序，从索引号1到9，参数i即索引号
    //根据索引为1的拼图碎片放置区域的方向确定可放置固定拼图碎片的位置
    //例如第一个拼图碎片放置区域的左和上两个位置可以放置固定拼图碎片
    //以索引为1的拼图碎片放置区域的位置作为起始参考位置，分别计算出其它位置的位置信息保存至列表mSrList中
    private void SimplePositionInit(int i, string dir)
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        switch (dir)
        {
            case "left":
                if (i == 1)
                {
                    mSrList.Add(new sSimplePosition(1, "left", x, y));
                }
                if (i == 4)
                {
                    mSrList.Add(new sSimplePosition(4, "left", x, y - mDistance));
                }
                if (i == 7)
                {
                    mSrList.Add(new sSimplePosition(7, "left", x, y - mDistance * 2));
                }
                break;
            case "up":
                if (i == 1)
                {
                    mSrList.Add(new sSimplePosition(1, "up", x + mWidth, y + mHeight));
                }
                if (i == 2)
                {
                    mSrList.Add(new sSimplePosition(2, "up", x + mWidth + mDistance, y + mHeight));
                }
                if (i == 3)
                {
                    mSrList.Add(new sSimplePosition(3, "up", x + mWidth + mDistance * 2, y + mHeight));
                }
                break;
            case "down":
                if (i == 7)
                {
                    mSrList.Add(new sSimplePosition(7, "down", x + mWidth, y - mDistance * 2 - mHeight));
                }
                if (i == 8)
                {
                    mSrList.Add(new sSimplePosition(8, "down", x + mWidth + mDistance, y - mDistance * 2 - mHeight));
                }
                if (i == 9)
                {
                    mSrList.Add(new sSimplePosition(9, "down", x + mWidth + mDistance * 2, y - mDistance * 2 - mHeight));
                }
                break;
            case "right":
                if (i == 3)
                {
                    mSrList.Add(new sSimplePosition(3, "right", x + mWidth * 2 + mDistance * 2, y));
                }
                if (i == 6)
                {
                    mSrList.Add(new sSimplePosition(6, "right", x + mWidth * 2 + mDistance * 2, y - mDistance));
                }
                if (i == 9)
                {
                    mSrList.Add(new sSimplePosition(9, "right", x + mWidth * 2 + mDistance * 2, y - mDistance * 2));
                }
                break;
            default:
                print("HGH:方向不存在！");
                break;
        }
    }

    //保存固定拼图碎片的位置至SimpleFragmentPosition.json文件中
    public void SavePosition()
    {
        SimplePositionInit(1, "left");
        SimplePositionInit(1, "up");
        SimplePositionInit(2, "up");
        SimplePositionInit(3, "up");
        SimplePositionInit(3, "right");
        SimplePositionInit(4, "left");
        SimplePositionInit(6, "right");
        SimplePositionInit(7, "left");
        SimplePositionInit(7, "down");
        SimplePositionInit(8, "down");
        SimplePositionInit(9, "down");
        SimplePositionInit(9, "right");
        if (File.Exists(mPath))
        {
            sSRList spl = new sSRList(mSrList);
            File.WriteAllText(mPath, toJsonStr(spl));
        }
        else
        {
            Debug.Log("HGH:存储SimpleFragmentPosition失败！！");
        }
    }

    private string toJsonStr(sSRList p_data)
    {
        return JsonUtility.ToJson(p_data);
    }
}

[System.Serializable]
public struct sSimplePosition
{
    public int index;
    public string direction;
    public double offsetX;
    public double offsetY;

    public sSimplePosition(int i, string dir, float x, float y)
    {
        index = i;
        direction = dir;
        offsetX = (double)x;
        offsetY = (double)y;
    }
}

[System.Serializable]
public struct sSRList
{
    public List<sSimplePosition> lists;

    public sSRList(List<sSimplePosition> lis)
    {
        lists = lis;
    }
}