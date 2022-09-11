using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class PlayerCameraController : ViewController, IPlayerCameraControllerFunc
{
    [Header("必要组件")]
    [Tooltip("角色Transform组件")]
    [SerializeField] private Transform cPlayer;
    [Header("必要属性")]
    [Tooltip("鼠标灵敏度")]
    [Range(0.1f, 1f)]
    [SerializeField] private float mMouseSensitivity;
    [Tooltip("纵轴向上视野角度")]
    [Range(-35f, 0f)]
    [SerializeField] private float mUangle;
    [Tooltip("纵轴向下视野角度")]
    [Range(0f, 35f)]
    [SerializeField] private float mDangle;
    private float mMouseX, mMouseY;
    private float xRotation = 0f;
    private bool isCheck = true;

    private void Update()
    {
        if (isCheck)
        {
            Rotation();
        }
    }

    //角色相机跟随鼠标旋转
    private void Rotation()
    {
        mMouseX = Input.GetAxis("Mouse X") * mMouseSensitivity * 1000 * Time.deltaTime;
        mMouseY = Input.GetAxis("Mouse Y") * mMouseSensitivity * 1000 * Time.deltaTime;
        xRotation -= mMouseY;
        xRotation = Mathf.Clamp(xRotation, mUangle, mDangle);
        cPlayer.Rotate(Vector3.up * mMouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    //设置鼠标灵敏度
    public void SetMouseSensitivity(float p_value)
    {
        if (p_value >= 0.1 && p_value <= 1)
        {
            mMouseSensitivity = p_value;
        }
    }

    //是否开启角色相机旋转功能
    public void SetIsCheck(bool ischeck)
    {
        isCheck = ischeck;
    }
}

public interface IPlayerCameraControllerFunc
{
    public void SetMouseSensitivity(float p_value);
    public void SetIsCheck(bool ischeck);
}

