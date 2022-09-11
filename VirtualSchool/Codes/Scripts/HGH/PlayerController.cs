using UnityEngine;
using EasyGame;

public class PlayerController : ViewController, IPlayerCFunc
{
    [Header("必要组件")]
    [Tooltip("触发器组件")]
    [SerializeField] private SphereTrigger cST;
    private PlayerAnimationController cPlayerAc;
    private CharacterController cCharacterController;
    [Header("必要属性")]
    [Tooltip("移动速度")]
    [SerializeField] private float mSpeed;
    [Tooltip("跳跃力度")]
    [SerializeField] private float mJumpForce;
    [Tooltip("重力")]
    [SerializeField] private float mGravity;
    private float mHorizontalMove;
    private float mVerticalMove;
    private Vector3 mDirection;
    private Vector3 mVelocity;
    private ISphereTrigger mIST;
    private ISphereTriggerFunc mISTF;
    private IPlayerAcFunc mIPAF;
    private bool isGround;
    private bool isJumpEnd = true;
    private bool isPlayerActive = true;

    private void Start()
    {
        cCharacterController = GetComponent<CharacterController>();
        mIST = cST as ISphereTrigger;
        mISTF = mIST as ISphereTriggerFunc;
        mIPAF = cPlayerAc as IPlayerAcFunc;
        Vector3 vec = transform.position;
        transform.position = new Vector3(vec.x, vec.y - 0.11f, vec.z);
    }

    private void Update()
    {
        if (isPlayerActive)
        {
            GroundCheck();
            Movement();
            Jump();
            Fall();
        }
    }

    //角色移动
    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        mHorizontalMove = hor * mSpeed;
        mIPAF.SetSpeedRate(Mathf.Abs(hor) + Mathf.Abs(ver));
        mVerticalMove = ver * mSpeed;
        mDirection = transform.forward * mVerticalMove + transform.right * mHorizontalMove;
        cCharacterController.Move(mDirection * Time.deltaTime);
    }

    //角色跳跃
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && isJumpEnd)
        {
            mVelocity.y = mJumpForce;
            isJumpEnd = false;
        }
    }

    //角色下落
    private void Fall()
    {
        if (isGround && mVelocity.y < 0)
        {
            mVelocity.y = -2f;
        }
        mVelocity.y -= mGravity * Time.deltaTime;
        cCharacterController.Move(mVelocity * Time.deltaTime);
    }

    //角色是否位于地面
    private void GroundCheck()
    {
        isGround = mISTF.TriggerCheck("Ground");
        if (isGround)
        {
            isJumpEnd = true;
        }
    }

    //是否启用角色控制功能
    public void SetIsPlayerActive(bool isActive)
    {
        isPlayerActive = isActive;
    }

    //设置角色动画控制器
    public void SetPlayerAc(PlayerAnimationController pac)
    {
        cPlayerAc = pac;
    }

}

public interface IPlayerCFunc
{
    public void SetIsPlayerActive(bool isActive);
    public void SetPlayerAc(PlayerAnimationController pac);
}

