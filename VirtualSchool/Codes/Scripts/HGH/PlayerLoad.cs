using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class PlayerLoad : ViewController, IPlayerLoadFunc
{
    [Header("必要组件")]
    [Tooltip("男主角")]
    [SerializeField] private GameObject cBoy;
    [Tooltip("男主角的角色相机")]
    [SerializeField] private GameObject cBoyCamera;
    [Tooltip("男主角的角色动画相机")]
    [SerializeField] private GameObject cBoyAnimationCamera;
    [Tooltip("女主角")]
    [SerializeField] private GameObject cGirl;
    [Tooltip("女主角的角色相机")]
    [SerializeField] private GameObject cGirlCamera;
    [Tooltip("女主角的角色动画相机")]
    [SerializeField] private GameObject cGirlAnimationCamera;
    private bool isBoy;

    private void Awake()
    {
        var pc = GetComponent<PlayerController>();
        var mIPCF = pc as IPlayerCFunc;
        var dressUpSys = GameManager.Get<SysDressUp>();
        //确定性别，按照性别激活相应的游戏对象、禁用与之相对的游戏对象以及设置相应的角色动画控制器
        if (dressUpSys.CurRole.Value == "boy")
        {
            isBoy = true;
            cGirl.SetActive(false);
            cBoy.SetActive(true);
            mIPCF.SetPlayerAc(cBoy.GetComponent<PlayerAnimationController>());
        }
        else
        {
            isBoy = false;
            cGirl.SetActive(true);
            cBoy.SetActive(false);
            mIPCF.SetPlayerAc(cGirl.GetComponent<PlayerAnimationController>());
        }
    }

    //根据性别获取相应的相机控制器
    public CameraController GetCameraController()
    {
        if (isBoy)
        {
            return cBoy.GetComponent<CameraController>();
        }
        else
        {
            return cGirl.GetComponent<CameraController>();
        }
    }

    //根据性别获取相应的角色相机
    public GameObject GetPlayerCamera()
    {
        if (isBoy)
        {
            return cBoyCamera;
        }
        else
        {
            return cGirlCamera;
        }
    }

    //根据性别获取相应的动画相机
    public GameObject GetAnimationCamera()
    {
        if (isBoy)
        {
            return cBoyAnimationCamera;
        }
        else
        {
            return cGirlAnimationCamera;
        }
    }
}

public interface IPlayerLoadFunc
{
    public CameraController GetCameraController();
    public GameObject GetPlayerCamera();
    public GameObject GetAnimationCamera();
}

