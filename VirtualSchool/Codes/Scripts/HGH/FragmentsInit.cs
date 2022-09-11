using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using System.IO;

public class FragmentsInit : ViewController, IFragmentsInitFunc
{
    private Transform[] cTransforms;
    private List<sPosition> mPositions = new List<sPosition>();
    private string mPath;
    private void Start()
    {
        mPath = "Text/FragmentPosition";
        cTransforms = transform.GetComponentsInChildren<Transform>(true);
    }

    //读取拼图碎片起始位置并将所有拼图碎片返回起始位置
    public void ReturnPosition()
    {
        var data = Resources.Load<TextAsset>(mPath);
        if (data != null)
        {
            sPosList datas = JsonUtility.FromJson<sPosList>(data.text);
            PositionInit(datas.posList);
        }
        else
        {
            Debug.Log("HGH:读取FragmentPosition失败！！");
        }
    }

    //所有拼图碎片返回起始位置
    private void PositionInit(List<sPosition> p_pos)
    {
        for (int i = 0; i < p_pos.Count; i++)
        {
            if (i == p_pos[i].index)
            {
                Vector3 vec = new Vector3((float)p_pos[i].offsetX, (float)p_pos[i].offsetY, (float)p_pos[i].offsetZ);
                cTransforms[i].localPosition = vec;
            }
        }
    }

    //保存所有拼图碎片的起始位置
    public void SavePosition()
    {
        int i = 0;
        foreach (var tf in cTransforms)
        {
            double[] posArray = { (double)tf.localPosition.x,
                                  (double)tf.localPosition.y,
                                  (double)tf.localPosition.z };
            mPositions.Add(new sPosition(i, posArray));
            i++;
        }
        if (File.Exists(mPath))
        {
            sPosList spl = new sPosList(mPositions);
            File.WriteAllText(mPath, toJsonStr(spl));
        }
        else
        {
            Debug.Log("HGH:存储FragmentPosition失败！！");
        }
    }

    private string toJsonStr(sPosList p_data)
    {
        return JsonUtility.ToJson(p_data);
    }
}

public interface IFragmentsInitFunc
{
    public void SavePosition();
    public void ReturnPosition();
}

[System.Serializable]
public struct sPosition
{
    public int index;
    public double offsetX;
    public double offsetY;
    public double offsetZ;

    public sPosition(int i, double[] positions)
    {
        index = i;
        offsetX = positions[0];
        offsetY = positions[1];
        offsetZ = positions[2];
    }

    public sPosition(int i, double x, double y, double z)
    {
        index = i;
        offsetX = x;
        offsetY = y;
        offsetZ = z;
    }
}

[System.Serializable]
public struct sPosList
{
    public List<sPosition> posList;

    public sPosList(List<sPosition> p_pos)
    {
        posList = p_pos;
    }
}

