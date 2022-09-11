using UnityEngine;
using EasyGame;

public class InteractionGameActive : ViewController, IInteractionGameActiveFunc
{
    [Header("必要组件")]
    [Tooltip("（引用）互动游戏相机")]
    [SerializeField] private GameObject cInteractionCamera;
    [Tooltip("（引用）角色相机")]
    [SerializeField] private GameObject cPlayerCamera;
    [Tooltip("（引用）角色动画相机控制器")]
    private GameObject cAnimationCamera;
    [Tooltip("（引用）角色加载器")]
    [SerializeField] private PlayerLoad cPlayerL;
    [Tooltip("（引用）角色控制器")]
    [SerializeField] private PlayerController cPlayerC;
    [Tooltip("（引用）相机控制器")]
    [SerializeField] private CameraController cCameraC;
    [Tooltip("（引用）退出点击")]
    [SerializeField] private ExitClick cExitC;
    [Tooltip("（引用）拼图检测")]
    [SerializeField] private FragmentCheck cFC;
    [Tooltip("中心点")]
    [SerializeField] private Transform cPoint;
    [Tooltip("（引用）提示文本")]
    [SerializeField] private GameObject cTip;
    [Tooltip("（引用）SimpleFixedFragment对象")]
    [SerializeField] private SimpleFixedFragment cSFF;
    private RectTransform cRectTransform;
    [Header("必要属性")]
    [Tooltip("对话对象名称")]
    [SerializeField] private string cDialogName;
    [Tooltip("检测层")]
    [SerializeField] private LayerMask mLayerMask;
    [Tooltip("x表示盒体长,y表示盒体高,z表示盒体宽")]
    [SerializeField] private Vector3 mBoxSize;
    private IPlayerCFunc mIPCF;
    private ICameraControllerFunc mICCF;
    private ISimpleFixedFragmentFunc mISFFF;
    private IPlayerLoadFunc mIPLF;
    private IPlayerCameraControllerFunc mIPCCF;
    private IExitClickFunc mIEC;
    private IFragmentCheckFunc mIFCF;
    private string mDefaultDialogName;
    private bool isCheck;   //限定更新执行TriggerCheck方法一次
    private bool isEnd;     //拼图游戏开始前的对话是否完成，完成则为true，反之则为false
    private bool isStart;   //限定更新执行拼图游戏通关后执行的方法一次
    private bool isSuccess;     //判断拼图游戏是否通关，通关则为true，反之则为false
    private bool isFinish;  //拼图游戏完成后的对话是否完成，完成则为true，反之则为false
    private bool isEnter;   //角色是否进入了游戏交互区域，是则为true，反之则为false
    private bool isActive = true;   //是否开启检测角色进入游戏交互区域的功能，开启则为true，反之则为false

    private void Start()
    {
        mIPCF = cPlayerC as IPlayerCFunc;
        mICCF = cCameraC as ICameraControllerFunc;
        mISFFF = cSFF as ISimpleFixedFragmentFunc;
        mIPLF = cPlayerL as IPlayerLoadFunc;
        cCameraC = mIPLF.GetCameraController();
        cPlayerCamera = mIPLF.GetPlayerCamera();
        mIPCCF = cPlayerCamera.GetComponent<PlayerCameraController>() as IPlayerCameraControllerFunc;
        cAnimationCamera = mIPLF.GetAnimationCamera();
        mIEC = cExitC as IExitClickFunc;
        mIFCF = cFC as IFragmentCheckFunc;
        mDefaultDialogName = cDialogName;
    }

    //对话激活（拼图前和拼图后）
    private void DialogActive(bool isChange)
    {
        if (isChange)
        {
            Self.SendCommand<CmdShowDialogPanel>(new DialogInfo("GameDialog", cDialogName, DialogueStyle.General, () => { isEnd = true; }));
        }
        else
        {
            Self.SendCommand<CmdShowDialogPanel>(new DialogInfo("GameDialog", cDialogName, DialogueStyle.General, () => { isFinish = true; }));
        }
        cRectTransform = GameObject.FindGameObjectWithTag("Dialog").GetComponent<RectTransform>();
        cRectTransform.anchoredPosition3D = Vector3.zero;
        cRectTransform.localScale = Vector3.one;
        cRectTransform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (!isCheck || isEnter))
        {
            TriggerCheck();
        }
        if (!isSuccess)
        {
            isSuccess = mIFCF.GetIsSuccess();
        }
        if ((Input.GetKeyDown(KeyCode.N) || isSuccess) && isStart)
        {
            cDialogName = "拼图游戏完成";
            cInteractionCamera.SetActive(false);
            cPlayerCamera.SetActive(true);
            DialogActive(false);
            isStart = false;
        }
        if (isFinish)
        {
            mIEC.SetIsFinish(true);
            isActive = true;
            isFinish = false;
            cDialogName = mDefaultDialogName;
        }
        if (isEnd)
        {
            cPlayerCamera.SetActive(false);
            cInteractionCamera.SetActive(true);
            isStart = true;
            isEnd = false;
        }
    }

    //角色进入激活对话提示区域
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = true;
            cTip.SetActive(true);
        }
    }

    ////角色退出激活对话提示区域
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = false;
            cTip.SetActive(false);
        }
    }

    //检测角色是否进入了游戏交互区域
    private void TriggerCheck()
    {
        var colliders = Physics.OverlapBox(cPoint.position, new Vector3(mBoxSize.x / 2f, mBoxSize.y / 2f, mBoxSize.z / 2f), Quaternion.identity, mLayerMask);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Player" && isActive)
            {
                mIPCF.SetIsPlayerActive(false);
                cAnimationCamera.SetActive(false);
                mICCF.SetIsCheck(false);
                mIPCCF.SetIsCheck(false);
                mISFFF.SetSimpleFrgIndex(Random.Range(1, 6));
                mISFFF.SimpleFragementActive();
                InteractionGameSet();
                isCheck = true;
                isActive = false;
                break;
            }
        }
    }

    //关闭提示开启对话
    private void InteractionGameSet()
    {
        cTip.SetActive(false);
        DialogActive(true);
    }

    //绘制检测区域
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cPoint.position, mBoxSize);
    }

    public void SetIsCheck(bool b)
    {
        isCheck = b;
    }

    public void SetIsEnd(bool isend)
    {
        isEnd = isend;
    }

    public void SetIsActive(bool isactive)
    {
        isActive = isactive;
    }
}

public interface IInteractionGameActiveFunc
{
    public void SetIsCheck(bool b);
    public void SetIsEnd(bool isend);
    public void SetIsActive(bool isactive);
}

