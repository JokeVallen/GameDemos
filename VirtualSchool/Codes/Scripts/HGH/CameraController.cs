using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class CameraController : ViewController, ICameraControllerFunc
{
    [Header("必要组件")]
    [Tooltip("动画相机控制器")]
    [SerializeField] private GameObject cAnimationCC;
    [Tooltip("角色相机控制器")]
    [SerializeField] private GameObject cPlayerCC;
    [Header("必要属性")]
    [Tooltip("开启相机动画控制时鼠标移动上限值")]
    [SerializeField] private float mMouseLim;
    [Tooltip("开启相机动画控制倒计时")]
    [SerializeField] private int mMouseTime;
    private int mCountDown;
    private Vector3 mMousePos;
    private bool isCameraActive = true;
    private bool isMove = true;
    private bool isStart;   //是否开启协程，是则为true，反之则为false
    private bool isEnd;     //用于限定更新调用执行一次的部分
    private bool isFinish;  //用于限定更新调用执行一次的部分
    private bool isCoroutineEnd = true;     //协程完成则为true，反之则为false
    private bool isUp = true;   //按键按下则为false，反之则为true
    private bool isCheck = true;    //是否开启相机控制检测功能，开启则为true，反之则为false

    private void Awake()
    {
        mMousePos = Input.mousePosition;
        mCountDown = mMouseTime;
    }

    private void Update()
    {
        if (isCheck)
        {
            OnInput();
            OnMode();
            if (isStart && isCoroutineEnd)
            {
                StartCoroutine("RotateCountDown");
                isStart = false;
                isCoroutineEnd = false;
            }
            UpdateFunc();
        }
    }

    //更新调用的方法
    private void UpdateFunc()
    {
        //按下按键
        if (!isUp)
        {
            cAnimationCC.SetActive(false);
            cPlayerCC.SetActive(true);
            StopCoroutine("RotateCountDown");
            isCoroutineEnd = true;
        }
        //移动鼠标
        if (isMove && !isFinish)
        {
            cAnimationCC.SetActive(false);
            cPlayerCC.SetActive(true);
            isEnd = false;
            StopCoroutine("RotateCountDown");
            isCoroutineEnd = true;
            isFinish = true;
        }
        //关闭角色相机，开启动画相机
        if (isEnd)
        {
            cPlayerCC.SetActive(false);
            cAnimationCC.SetActive(true);
            isEnd = false;
        }
    }

    //检测鼠标移动
    private void OnMode()
    {
        Vector3 vec = Input.mousePosition;
        float x = Mathf.Abs(vec.x - mMousePos.x);
        float y = Mathf.Abs(vec.y - mMousePos.y);
        if (x >= mMouseLim || y >= mMouseLim)
        {
            isUp = true;
            isMove = true;
            isStart = false;
        }
        if (x == 0 && y == 0)
        {
            isMove = false;
            isFinish = false;
            isStart = true;
        }
        mMousePos = vec;
    }

    //检测按键输入
    private bool OnInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
        {
            cAnimationCC.SetActive(false);
            isUp = false;
            return isUp;
        }
        if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.Space))
        {
            isUp = true;
            return isUp;
        }
        return isUp;
    }

    //计时协程
    IEnumerator RotateCountDown()
    {
        while (mCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            mCountDown -= 1;
        }
        mCountDown = mMouseTime;
        isEnd = true;
    }

    //是否开启相机控制检测功能
    public void SetIsCheck(bool ischeck)
    {
        isCheck = ischeck;
    }
}

public interface ICameraControllerFunc
{
    public void SetIsCheck(bool ischeck);
}

