using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using UnityEngine.UI;

public class MenuInGameActive : ViewController, IMIGAFunc
{
    [Header("必要组件")]
    [Tooltip("（引用）角色加载器")]
    [SerializeField] private PlayerLoad cPlayerL;
    [Tooltip("（引用）局内游戏菜单")]
    [SerializeField] private GameObject cGameMenu;
    [Tooltip("（引用）角色相机")]
    [SerializeField] private GameObject cPlayerCamera;
    private PlayerCameraController cPlayerCameraC;
    private IPlayerCameraControllerFunc mIPCCF;
    private IPlayerLoadFunc mIPLF;
    private bool isActive = true;
    private bool isUse = true;

    private void Start()
    {
        mIPLF = cPlayerL as IPlayerLoadFunc;
        cPlayerCamera = mIPLF.GetPlayerCamera();
        cPlayerCameraC = cPlayerCamera.GetComponent<PlayerCameraController>();
        mIPCCF = cPlayerCameraC as IPlayerCameraControllerFunc;
    }

    private void Update()
    {
        if (isUse)
        {
            ChangeCamera(false);
            isUse = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isUse)
        {
            ChangeCamera(isActive);
        }
    }

    private void ChangeCamera(bool isChange)
    {
        if (isChange)
        {
            mIPCCF.SetIsCheck(false);
            //发送激活局内菜单命令
            cGameMenu.SetActive(true);
            isActive = false;
        }
        else
        {
            cGameMenu.SetActive(false);
            //发送关闭局内菜单命令
            mIPCCF.SetIsCheck(true);
            isActive = true;
        }
    }

    public void SetIsUse(bool isuse)
    {
        isUse = isuse;
    }
}

public interface IMIGAFunc
{
    public void SetIsUse(bool isuse);
}

