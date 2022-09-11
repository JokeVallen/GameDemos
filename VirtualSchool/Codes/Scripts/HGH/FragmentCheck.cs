using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

//用于检测玩家当前拼图是否达到通关条件
public class FragmentCheck : ViewController, IFragmentCheckFunc
{
    [Header("必要组件")]
    [Tooltip("碎片吸附")]
    [SerializeField] private FragmentAdsorbent cFA;
    [Tooltip("固定碎片生成")]
    [SerializeField] private SimpleFixedFragment cSFF;
    private ISimpleFixedFragmentFunc mISFFF;
    private IFragmentFunc mIFF;
    private List<sNowColor> mListNowColor;
    private List<sListFragmentColor> mListSuccessColor;
    private bool isCheck;   //用于启用和关闭检测功能，为true则表示启用，为false则表示关闭
    private int mFixedIndex = 0;
    private bool isRight;   //为true则表示达到检测条件，反之则未达到检测条件
    private string mPath;
    private bool isSuccess;     //为true则表示通关，反之则表示未达到通关条件

    private void Start()
    {
        mPath = "Text/FragmentSuccess";
        mISFFF = cSFF as ISimpleFixedFragmentFunc;
        mIFF = cFA as IFragmentFunc;
        mListNowColor = new List<sNowColor>();
    }

    private void Update()
    {
        if (isCheck)
        {
            BeforeCheck();
            Check();
            isCheck = false;
        }
    }

    //检测前执行方法
    private void BeforeCheck()
    {
        mFixedIndex = mISFFF.GetMIndex();//获取固定起始碎片序号（用于确定读取哪一组检测数据）
        RepeateCheck();//列表去重检测
        if (mListNowColor?.Count == 9)
        {
            SortCheck();//列表排序
            isRight = true;//如果去重后的列表中存在9个元素则表明达到检测条件
        }
    }

    //检测
    private void Check()
    {
        if (isRight)
        {
            //根据固定起始碎片序号读取json文件获得检测数据
            var datas = ReadData();
            foreach (var data in datas)
            {
                if (data.mFixedIndex == mFixedIndex)
                {
                    mListSuccessColor.Add(new sListFragmentColor(data.mListColor));
                }
            }
            //检测数据与当前列表进行对比，若吻合则表明拼图成功
            if (mListSuccessColor != null)
            {
                isSuccess = false;
                foreach (var item in mListSuccessColor)
                {
                    for (int i = 0; i < mListNowColor.Count; i++)
                    {
                        var nowC = mListNowColor[i].mColor;
                        var sucC = item.mListColor[i];
                        bool lef = nowC.mLeftC.Equals(sucC.mLeftC);
                        bool up = nowC.mUpC.Equals(sucC.mUpC);
                        bool rig = nowC.mRightC.Equals(sucC.mRightC);
                        bool dow = nowC.mDownC.Equals(sucC.mDownC);
                        if (nowC.mIndex != sucC.mIndex || !lef || !up || !rig || !dow)
                        {
                            isSuccess = false;
                        }
                    }
                }
            }
            Debug.Log(isSuccess);
        }
    }

    private List<sListFragmentColor> GetAllSuccess(List<sFragmentColor> p_list)
    {
        List<sListFragmentColor> vList = new List<sListFragmentColor>();

        return vList;
    }

    private List<sSwapSuccess> InitSwapSuccess(List<sFragmentColor> p_list)
    {
        List<sSwapSuccess> vList = new List<sSwapSuccess>();
        return vList;
    }

    private List<int> FindSwapSuccessIndex(List<sFragmentColor> p_list, int p_index)
    {
        List<int> vList = new List<int>();
        switch (p_index)
        {
            case 1:
            case 4:
            case 2:
            case 6:
            case 3:
            case 5:
            case 7:
            case 9:
            default:
                return null;
        }
    }

    //传入索引号执行Swap方法获取并返回交换后的List<sFragmentColor>
    private List<sFragmentColor> SwapSuccess(List<sFragmentColor> p_list, int p_index)
    {
        switch (p_index)
        {
            case 1:
                return Swap(p_list, 1, 4);
            case 4:
                return Swap(p_list, 4, 1);
            case 2:
                return Swap(p_list, 2, 6);
            case 6:
                return Swap(p_list, 6, 2);
            case 3:
                return Swap(p_list, 3, 5);
            case 5:
                return Swap(p_list, 5, 3);
            case 7:
                return Swap(p_list, 7, 9);
            case 9:
                return Swap(p_list, 9, 7);
            default:
                return null;
        }
    }

    //获取根据索引交换后的List<sFragmentColor>
    private List<sFragmentColor> Swap(List<sFragmentColor> p_list, int p_indexA, int p_indexB)
    {
        List<int> ListIndex = new List<int>();
        List<sFragmentColor> vList = new List<sFragmentColor>();
        for (int i = 0; i < p_list.Count; i++)
        {
            vList.Add(p_list[i]);
            if (p_list[i].mIndex == p_indexA || p_list[i].mIndex == p_indexB)
            {
                ListIndex.Add(i);
            }
        }
        if (ListIndex.Count == 2)
        {
            var indexA = ListIndex[0];
            var indexB = ListIndex[1];
            var swapValue = vList[indexA];
            vList[indexA] = vList[indexB];
            vList[indexB] = swapValue;
        }
        return vList;
    }

    //读取拼图成功的数据
    private List<sSimpleFixedSuccess> ReadData()
    {
        var data = Resources.Load<TextAsset>(mPath);
        if (data != null)
        {
            sSuccessList datas = JsonUtility.FromJson<sSuccessList>(data.text);
            return datas.mListSuccess;
        }
        else
        {
            Debug.Log("HGH:读取FragmentSuccess失败！！");
            return null;
        }
    }

    //去重检测
    private void RepeateCheck()
    {
        List<int> ListIndex = new List<int>();
        for (int i = 0; i < mListNowColor?.Count; i++)
        {
            for (int j = i + 1; j < mListNowColor?.Count; j++)
            {
                var vAdIndex1 = mListNowColor[i].mAdsorbentIndex;
                var vAdIndex2 = mListNowColor[j].mAdsorbentIndex;
                var vCoIndex1 = mListNowColor[i].mColor.mIndex;
                var vCoIndex2 = mListNowColor[j].mColor.mIndex;
                if (vAdIndex1 == vAdIndex2 && vCoIndex1 == vCoIndex2)
                {
                    Debug.Log("Re:" + i);
                    ListIndex.Add(i);
                }
            }
        }
        foreach (var i in ListIndex)
        {
            mListNowColor.RemoveAt(i);
        }
    }

    //排序检测
    private void SortCheck()
    {
        List<sNowColor> vList = new List<sNowColor>();
        List<int> vIndex = new List<int>();
        for (int i = 1; i <= mListNowColor?.Count; i++)
        {
            var index = FindIndex(i);
            if (index != 100)
            {
                vIndex.Add(index);
            }
        }
        foreach (var i in vIndex)
        {
            vList.Add(mListNowColor[i]);
        }
        mListNowColor.Clear();
        foreach (var v in vList)
        {
            mListNowColor.Add(v);
        }
    }

    //根据拼图碎片吸附区域的索引号获取其在mListNowColor中的索引号，不存在则返回100
    private int FindIndex(int p_adsorbentIndex)
    {
        for (int i = 0; i < mListNowColor?.Count; i++)
        {
            if (mListNowColor[i].mAdsorbentIndex == p_adsorbentIndex)
            {
                return i;
            }
        }
        return 100;
    }

    public void SetIsCheck(bool ischeck)
    {
        isCheck = ischeck;
    }

    public void AddColorToList(sFragmentColor p_sfc)
    {
        mListNowColor.Add(new sNowColor(mIFF.GetAdsorbentIndex(), p_sfc));
        // Debug.Log("Count:" + mListNowColor.Count);
        // foreach (var item in mListNowColor)
        // {
        //     Debug.Log("XX:" + item.mAdsorbentIndex);
        // }
    }

    //获取是否通关
    public bool GetIsSuccess()
    {
        return isSuccess;
    }
}

public interface IFragmentCheckFunc
{
    public void SetIsCheck(bool ischeck);
    public void AddColorToList(sFragmentColor p_sfc);
    public bool GetIsSuccess();
}

[System.Serializable]
public struct sFragmentColor
{
    public int mIndex;
    public string mLeftC;
    public string mUpC;
    public string mRightC;
    public string mDownC;

    public sFragmentColor(int p_index, string p_leftC, string p_upC, string p_rightC, string p_downC)
    {
        mIndex = p_index;
        mLeftC = p_leftC;
        mUpC = p_upC;
        mRightC = p_rightC;
        mDownC = p_downC;
    }
}
[System.Serializable]
public struct sListFragmentColor
{
    public List<sFragmentColor> mListColor;

    public sListFragmentColor(List<sFragmentColor> p_list)
    {
        mListColor = p_list;
    }
}
[System.Serializable]
public struct sSwapSuccess
{
    public int mIndexA;
    public int mAdSorbentIndexA;
    public int mIndexB;
    public int mAdSorbentIndexB;
    public List<sFragmentColor> mListSwapSuccess;

    public sSwapSuccess(int p_indexA, int p_adsorbentIndexA, int p_indexB, int p_adsorbentIndexB, List<sFragmentColor> p_list)
    {
        mIndexA = p_indexA;
        mAdSorbentIndexA = p_adsorbentIndexA;
        mIndexB = p_indexB;
        mAdSorbentIndexB = p_adsorbentIndexB;
        mListSwapSuccess = p_list;
    }
}
[System.Serializable]
public struct sNowColor
{
    public int mAdsorbentIndex;
    public sFragmentColor mColor;
    public sNowColor(int p_index, sFragmentColor p_color)
    {
        mAdsorbentIndex = p_index;
        mColor = p_color;
    }
}

