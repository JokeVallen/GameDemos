using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class SimpleFixedFragment : ViewController, ISimpleFixedFragmentFunc
{
    [Header("必要组件")]
    [Tooltip("固定起始和终止碎片组")]
    [SerializeField] private GameObject[] cSimpleFragments;

    private int mIndex = 0;

    public int GetMIndex()
    {
        return mIndex;
    }

    //1.设置Index
    //目前测试能够拼出来的不重复的组数总共5组，这个索引代表其中一组的索引号
    //这5组的固定拼图碎片保存在cSimpleFragments中，按照顺序每两个分为一组，总共5组
    public void SetSimpleFrgIndex(int p_i)
    {
        //p_i取值范围[1,5]区间内的整数
        if (p_i - 1 >= 0 && p_i - 1 < 5)
        {
            mIndex = p_i - 1;
        }
    }

    //2.调用激活函数
    //根据索引来激活对应的固定拼图碎片组
    public void SimpleFragementActive()
    {
        var i = 0 + mIndex * 2;
        cSimpleFragments[i].SetActive(true);
        cSimpleFragments[i + 1].SetActive(true);
    }

    //3.调用禁用函数
    //根据索引来禁用对应的固定拼图碎片组
    public void SimpleFragementUnActive()
    {
        var i = 0 + mIndex * 2;
        cSimpleFragments[i].SetActive(false);
        cSimpleFragments[i + 1].SetActive(false);
    }

}

public interface ISimpleFixedFragmentFunc
{
    public void SetSimpleFrgIndex(int p_i);
    public void SimpleFragementActive();
    public void SimpleFragementUnActive();
    public int GetMIndex();
}

