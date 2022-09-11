using UnityEngine;
using EasyGame;
using System.Collections;

public class PlayerAnimationController : ViewController, IPlayerAcFunc
{
    [Header("必要组件")]
    [Tooltip("相机控制器")]
    [SerializeField] private AnimationCameraController cCC;
    [Header("必要属性")]
    [Tooltip("站立转闲置动画时间")]
    [SerializeField] private int mTime;
    private Animator cAnimt;
    private IAnimationCameraCotrollerFunc mICCF;
    private int mCountDown;
    private bool isActive;

    private void Awake()
    {
        Self.EventRegister<EventDressUp>(e => e.Excute(this.transform));
        Self.SendCommand<CmdInitializeDressUp>(0);
    }

    private void Start()
    {
        cAnimt = GetComponent<Animator>();
        mICCF = cCC as IAnimationCameraCotrollerFunc;
        mCountDown = mTime;
    }

    private void Update()
    {
        if (isActive)
        {
            mICCF.AnimationRotate();
        }
    }

    //设置动画控制器过渡参数SpeedRate
    public void SetSpeedRate(float speedRate)
    {
        cAnimt.SetFloat("SpeedRate", speedRate);
    }

    //设置动画控制器过渡参数RandomAnimation
    private void RandomAnimation()
    {
        cAnimt.SetInteger("RandomAnimation", Random.Range(1, 4));
    }

    //随机闲置动画结束事件
    private void RandomAnimationEndEvent()
    {
        isActive = false;
        cAnimt.SetInteger("RandomAnimation", 0);
    }

    //启用闲置动画
    private void RandomAnimationActiveEvent()
    {
        StartCoroutine(IdleToRandomAnimation());
    }

    //启用动画相机旋转
    private void AnimationRotateEvent()
    {
        isActive = true;
    }

    IEnumerator IdleToRandomAnimation()
    {
        while (mCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            mCountDown -= 1;
        }
        RandomAnimation();
        mCountDown = mTime;
    }
}

public interface IPlayerAcFunc
{
    public void SetSpeedRate(float speedRate);
}

