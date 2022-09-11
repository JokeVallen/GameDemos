using UnityEngine;
using EasyGame;

//点击拼图碎片时执行的方法
public class FragmentClick : ViewController
{
    [Header("必要组件")]
    [Tooltip("（引用）拼图碎片吸附组件")]
    [SerializeField] private FragmentAdsorbent mFA;
    [Tooltip("（引用）拼图碎片初始化")]
    [SerializeField] private FragmentsInit mFI;
    [Header("必要属性")]
    [Tooltip("拼图碎片序号")]
    [SerializeField] private int mIndex;
    [Tooltip("四个方向的颜色(顺时针从左开始),黄绿紫橙分别为y、g、p、o")]
    [SerializeField] private string mLeftC;
    [SerializeField] private string mUpC;
    [SerializeField] private string mRightC;
    [SerializeField] private string mDownC;

    private IFragmentsInitFunc mIFIF;
    private IFragmentFunc mIFF;
    private IFragmentParameter mIFP;
    private sFragmentColor sFragmentC;
    private bool isMouseDown;
    private bool isEnter;

    private void Start()
    {
        mIFIF = mFI as IFragmentsInitFunc;
        mIFF = mFA as IFragmentFunc;
        mIFP = mFA as IFragmentParameter;
    }

    private void Update()
    {
        if (isMouseDown)
        {
            Movement();
        }
    }

    //鼠标进入
    private void OnMouseEnter()
    {
        MouseCheck();
    }

    //鼠标悬停
    private void OnMouseOver()
    {
        MouseCheck();
    }

    //鼠标检测，实现长按鼠标左键时拼图碎片跟随鼠标移动，松开鼠标左键时拼图碎片返回起始位置
    //实现点击鼠标右键时拼图碎片顺时针旋转90度
    //存在bug：应当为当前控制的拼图碎片未进入检测区域时则返回起始位置，而不是所有拼图碎片回到起始位置
    private void MouseCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0, 0, -90);
            ColorChange();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Get<EasyMessage>().Send("FragmentTransform", transform);
            sFragmentC = new sFragmentColor(mIndex, mLeftC, mUpC, mRightC, mDownC);
            GameManager.Get<EasyMessage>().Send("FragmentColor", sFragmentC);
            isMouseDown = false;
            mIFP.isFinish = false;
            mIFF.Check();
            isEnter = mIFF.GetIsEnter();
            ReturnStatus();
        }
    }

    //拼图碎片旋转时更新记录其四个方向颜色的变量
    private void ColorChange()
    {
        var lc = mLeftC;
        var uc = mUpC;
        var rc = mRightC;
        var dc = mDownC;

        mLeftC = dc;
        mUpC = lc;
        mRightC = uc;
        mDownC = rc;
    }

    //返回初始状态
    private void ReturnStatus()
    {
        if (!isEnter)
        {
            mIFIF.ReturnPosition();
        }
    }

    //拼图碎片给随鼠标移动
    private void Movement()
    {
        //获取需要移动物体的世界转屏幕坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //获取鼠标位置
        Vector3 mousePos = Input.mousePosition;
        //因为鼠标只有X，Y轴，所以要赋予给鼠标Z轴
        mousePos.z = screenPos.z;
        //把鼠标的屏幕坐标转换成世界坐标
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //控制物体移动
        transform.position = worldPos;
    }
}

