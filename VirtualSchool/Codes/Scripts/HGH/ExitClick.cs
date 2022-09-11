using UnityEngine;
using EasyGame;

public class ExitClick : ViewController, IExitClickFunc
{
    [Header("必要组件")]
    [Tooltip("（引用）互动游戏相机")]
    [SerializeField] private GameObject cInteractionCamera;
    [Tooltip("（引用）角色相机")]
    private GameObject cPlayerCamera;
    [Tooltip("（引用）角色加载器")]
    [SerializeField] private PlayerLoad cPlayerL;
    [Tooltip("（引用）角色控制器")]
    [SerializeField] private PlayerController cPlayerC;
    [Tooltip("（引用）相机控制器")]
    [SerializeField] private CameraController cCameraC;
    [Tooltip("（引用）拼图碎片总游戏对象")]
    [SerializeField] private FragmentsInit cFragment;
    [Tooltip("（引用）拼图碎片总游戏对象")]
    [SerializeField] private InteractionGameActive cInteractionGameActive;
    [Tooltip("（引用）SimpleFixedFragment对象")]
    [SerializeField] private SimpleFixedFragment cSFF;
    private RectTransform cRectTransform;
    private IFragmentsInitFunc mIFIF;
    private IInteractionGameActiveFunc mIIGF;
    private IPlayerCFunc mIPCF;
    private ICameraControllerFunc mICCF;
    private ISimpleFixedFragmentFunc mISFFF;
    private IPlayerLoadFunc mIPLF;
    private IPlayerCameraControllerFunc mIPCCF;
    private bool isEnd;     //限定某事件执行一次，为true时可执行，为false时不可执行
    private bool isFinish;  //限定点击事件执行一次，为true时可执行，为false时不可执行

    private void Awake()
    {
        mIFIF = cFragment as IFragmentsInitFunc;
        mIIGF = cInteractionGameActive as IInteractionGameActiveFunc;
        mIPCF = cPlayerC as IPlayerCFunc;
        mICCF = cCameraC as ICameraControllerFunc;
        mISFFF = cSFF as ISimpleFixedFragmentFunc;
        mIPLF = cPlayerL as IPlayerLoadFunc;
        cCameraC = mIPLF.GetCameraController();
        cPlayerCamera = mIPLF.GetPlayerCamera();
        PlayerCameraController pcc = cPlayerCamera.GetComponent<PlayerCameraController>();
        mIPCCF = pcc as IPlayerCameraControllerFunc;
    }

    private void Update()
    {
        if (isFinish)
        {
            ClickEvent();
            isFinish = false;
        }
        if (isEnd)
        {
            mICCF.SetIsCheck(true);
            mIPCCF.SetIsCheck(true);
            isEnd = false;
        }
    }

    //鼠标进入
    private void OnMouseEnter()
    {
        MouseClick();
    }

    //鼠标悬停
    private void OnMouseOver()
    {
        MouseClick();
    }

    //鼠标点击
    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("ClickEvent", 0.3f);
        }
    }

    //退出按钮点击事件
    private void ClickEvent()
    {
        mISFFF.SimpleFragementUnActive();
        mIPCF.SetIsPlayerActive(true);
        mICCF.SetIsCheck(true);
        cInteractionCamera.SetActive(false);
        mIFIF.ReturnPosition();
        mIIGF.SetIsCheck(false);
        mIIGF.SetIsActive(true);
        cPlayerCamera.SetActive(true);
        mIPCCF.SetIsCheck(true);
    }

    //激活对话
    private void DialogActive()
    {
        Self.SendCommand<CmdShowDialogPanel>(new DialogInfo("GameDialog", "拼图游戏完成", DialogueStyle.General, () => { isEnd = true; }));
        cRectTransform = GameObject.FindGameObjectWithTag("Dialog").GetComponent<RectTransform>();
        cRectTransform.anchoredPosition3D = Vector3.zero;
        cRectTransform.localScale = Vector3.one;
        cRectTransform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetIsFinish(bool isfinish)
    {
        isFinish = isfinish;
    }
}

public interface IExitClickFunc
{
    public void SetIsFinish(bool isfinish);
}

