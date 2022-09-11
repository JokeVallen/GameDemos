using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using System.IO;

public class FragmentSuccess : ViewController
{
    [Header("必要属性")]
    [Tooltip("拼图成功记录数据(不需要检测的则设为n),黄绿紫橙分别为y、g、p、o")]
    [SerializeField] private List<sSimpleFixedSuccess> mSimpleFixedSuccess;
    private string mPath;

    private void Start()
    {
        //mPath = Application.streamingAssetsPath + "/FragmentSuccess.json";
    }

    //保存拼图成功的数据
    private void SaveData()
    {
        if (File.Exists(mPath))
        {
            File.WriteAllText(mPath, toJsonStr(new sSuccessList(mSimpleFixedSuccess)));
        }
        else
        {
            Debug.Log("HGH:存储FragmentSuccess失败！！");
        }
    }

    private string toJsonStr(sSuccessList p_data)
    {
        return JsonUtility.ToJson(p_data);
    }
}

[System.Serializable]
public struct sSuccessList
{
    public List<sSimpleFixedSuccess> mListSuccess;
    public sSuccessList(List<sSimpleFixedSuccess> p_list)
    {
        mListSuccess = p_list;
    }
}

[System.Serializable]
public struct sSimpleFixedSuccess
{
    public int mFixedIndex;
    public List<sFragmentColor> mListColor;

    public sSimpleFixedSuccess(int p_index, List<sFragmentColor> p_list)
    {
        mFixedIndex = p_index;
        mListColor = p_list;
    }
}

