using UnityEngine;
using EasyGame;

//拼图碎片吸附
public class FragmentAdsorbent : ViewController, IFragmentFunc, IFragmentParameter
{
    [Header("必要组件")]
    [Tooltip("吸附区中心点")]
    [SerializeField] private Transform[] cPoints;
    [Tooltip("拼图检测")]
    [SerializeField] private FragmentCheck cFragmentCheck;
    private FragmentAdsorbentTrigger cFAT;
    private FindBestPoint cFBP;
    private Transform cTF;
    private IFragmentAdsorbentTriggerFunc mIFAT;
    private IFindBestPointFunc mIFBPF;
    private IFragmentCheckFunc mIFCF;
    public bool isEnter { get; set; }
    public bool isFinish { get; set; }
    private sFragmentColor sFragmentC;
    private int mAdsorbentIndex = 0;

    private void Start()
    {
        cFAT = GetComponent<FragmentAdsorbentTrigger>();
        cFBP = GetComponent<FindBestPoint>();
        mIFAT = cFAT as IFragmentAdsorbentTriggerFunc;
        mIFBPF = cFBP as IFindBestPointFunc;
        mIFCF = cFragmentCheck as IFragmentCheckFunc;
        GameManager.Get<EasyMessage>().AddListener("FragmentTransform", (e) =>
        {
            cTF = (Transform)e;
        });
        GameManager.Get<EasyMessage>().AddListener("FragmentColor", (e) =>
        {
            sFragmentC = (sFragmentColor)e;
        });
    }

    private void Update()
    {
        //检测拼图碎片是否进入吸附检测区域
        isEnter = mIFAT.TriggerCheck();
    }

    //拼图碎片进入吸附检测区域则开启检测获取最佳吸附位置
    public void Check()
    {
        if (isEnter && !isFinish)
        {
            Adsorbent();
            mIFCF.AddColorToList(sFragmentC);
            mIFCF.SetIsCheck(true);
        }
    }

    //拼图碎片吸附至最佳吸附位置
    private void Adsorbent()
    {
        if (cTF != null)
        {
            Vector2 vec = mIFBPF.FindPoint(cPoints, cTF);
            mAdsorbentIndex = mIFBPF.GetAdsorbentIndex();
            cTF.position = new Vector3(vec.x, vec.y, cTF.position.z);
            isFinish = true;
        }
    }

    //获取拼图碎片是否进入吸附检测区域
    public bool GetIsEnter()
    {
        return isEnter;
    }

    //获取吸附区域的索引号
    public int GetAdsorbentIndex()
    {
        return mAdsorbentIndex;
    }
}

public interface IFragmentParameter
{
    public bool isEnter { get; set; }
    public bool isFinish { get; set; }
}

public interface IFragmentFunc
{
    public void Check();
    public bool GetIsEnter();
    public int GetAdsorbentIndex();
}

