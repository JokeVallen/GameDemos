using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class MouseInput : ViewController
{
    [Header("必要组件")]
    [Tooltip("跟随鼠标对象")]
    [SerializeField] private Transform cTarget;
    [Header("必要属性")]
    [Tooltip("鼠标灵敏度")]
    [Range(0.01f, 1f)]
    [SerializeField] private float mRotateSpeedY;
    [Tooltip("左边界点X坐标值")]
    [SerializeField] private float mLeftPointX;
    [Tooltip("右边界点X坐标值")]
    [SerializeField] private float mRightPointX;

    private void Update()
    {
        MouseCheck();
    }

    //鼠标检测，相机跟随鼠标进行视野旋转
    private void MouseCheck()
    {
        Vector3 targetVec = Camera.main.WorldToScreenPoint(cTarget.position);
        Vector3 mousePosition = Input.mousePosition;
        float x = mousePosition.x;
        float y = mousePosition.y;
        if (x >= 0 && x <= Screen.width && y >= 0 && y <= Screen.height)
        {
            mousePosition.z = targetVec.z;
            Vector3 vec = Camera.main.ScreenToWorldPoint(mousePosition);
            cTarget.position = vec;
            CameraViewRotate();
        }
    }

    //相机旋转
    private void CameraViewRotate()
    {
        float vecX = cTarget.localPosition.x;
        if (vecX <= mLeftPointX)
        {
            float distanceX = vecX - mLeftPointX;
            transform.Rotate(0, distanceX * mRotateSpeedY, 0);
        }
        if (vecX >= mRightPointX)
        {
            float distanceX = vecX - mRightPointX;
            transform.Rotate(0, distanceX * mRotateSpeedY, 0);
        }
    }
}

