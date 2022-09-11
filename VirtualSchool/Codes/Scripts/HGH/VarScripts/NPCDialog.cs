using UnityEngine;
using EasyGame;

//NPC对话
public class NPCDialog : ViewController
{
    [Header("必要组件")]
    [Tooltip("（引用）角色加载器")]
    [SerializeField] private PlayerLoad cPlayerL;
    [Tooltip("（引用）角色控制器")]
    [SerializeField] private PlayerController cPlayerC;
    [Tooltip("（引用）提示文本")]
    [SerializeField] private GameObject cTip;
    [Header("必要属性")]
    [Tooltip("对话对象名称")]
    [SerializeField] private string cDialogName;
    private CameraController cCameraC;
    private GameObject cAnimationCamera;
    private GameObject cPlayerCamera;
    private RectTransform cRectTransform;
    private ICameraControllerFunc mICCF;
    private IPlayerLoadFunc mIPLF;
    private IPlayerCameraControllerFunc mIPCCF;
    private IPlayerCFunc mIPCF;
    private bool isStart;
    private bool isEnter;
    private bool isEnd;
    private bool isFinish = true;

    private void Start()
    {
        mIPLF = cPlayerL as IPlayerLoadFunc;
        cAnimationCamera = mIPLF.GetAnimationCamera();
        cCameraC = mIPLF.GetCameraController();
        cPlayerCamera = mIPLF.GetPlayerCamera();
        mIPCCF = cPlayerCamera.GetComponent<PlayerCameraController>() as IPlayerCameraControllerFunc;
        mIPCF = cPlayerC as IPlayerCFunc;
        mICCF = cCameraC as ICameraControllerFunc;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEnter && (!isStart || isFinish))
        {
            DialogActive();
        }
        if (isEnd)
        {
            mIPCF.SetIsPlayerActive(true);
            mICCF.SetIsCheck(true);
            mIPCCF.SetIsCheck(true);
            isFinish = true;
            isEnd = false;
        }
    }

    //角色是否进入可对话区域
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = true;
            cTip.SetActive(true);
        }
    }

    //角色是否退出可对话区域
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = false;
            cTip.SetActive(false);
        }
    }

    //激活对话
    private void DialogActive()
    {
        if (isFinish)
        {
            isFinish = false;
            mIPCF.SetIsPlayerActive(false);
            cAnimationCamera.SetActive(false);
            mICCF.SetIsCheck(false);
            mIPCCF.SetIsCheck(false);
            Self.SendCommand<CmdShowDialogPanel>(new DialogInfo("GameDialog", cDialogName, DialogueStyle.General, () => { isEnd = true; }));
            cRectTransform = GameObject.FindGameObjectWithTag("Dialog").GetComponent<RectTransform>();
            cRectTransform.anchoredPosition3D = Vector3.zero;
            cRectTransform.localScale = Vector3.one;
            cRectTransform.localRotation = Quaternion.Euler(Vector3.zero);
            isStart = true;
        }
    }
}

