using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using UnityEngine.SceneManagement;

//与图书馆外部NPC对话完成进入图书馆内部
public class EnterLib : ViewController
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
    private bool isEnd;
    private bool isStart;
    private bool isEnter;

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
        if (Input.GetKeyDown(KeyCode.E) && !isStart && isEnter)
        {
            DialogActive();
        }
        if (isEnd)
        {
            SceneManager.LoadScene("inside");
            isEnd = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = true;
            cTip.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnter = false;
            cTip.SetActive(false);
        }
    }

    private void DialogActive()
    {
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

