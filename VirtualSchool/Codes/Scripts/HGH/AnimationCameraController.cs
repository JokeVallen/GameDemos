using UnityEngine;
using EasyGame;

public class AnimationCameraController : ViewController, IAnimationCameraCotrollerFunc
{
    [Header("必要组件")]
    [Tooltip("头部Transform组件")]
    [SerializeField] private Transform cHeadTrans;
    private Vector3 mHeadDefaultRotation;
    private Vector3 mHeadRotationChange;
    private Vector3 mCameraEulerAngle;

    private void Start()
    {
        Vector3 vecL = cHeadTrans.localRotation.eulerAngles;
        vecL = RotationTransform(vecL);
        mHeadDefaultRotation = vecL;
    }

    private void Update()
    {
        UpdateFunc();
    }

    //更新调用方法
    private void UpdateFunc()
    {
        //获取头部Transform的Rotation并进行标准化操作
        Vector3 headVec = RotationTransform(cHeadTrans.localRotation.eulerAngles);
        //获取头部Rotation的变化值并进行标准化操作
        mHeadRotationChange = RotationTransform(headVec - mHeadDefaultRotation);
        mHeadDefaultRotation = headVec;
        //获取动画相机的Rotation并进行标准化操作
        Vector3 vecC = transform.localRotation.eulerAngles;
        vecC = RotationTransform(vecC);
        mCameraEulerAngle = vecC;
    }

    //用于对Rotation的角度进行标准化操作
    private Vector3 RotationTransform(Vector3 eulerAngle)
    {
        Vector3 vec = eulerAngle;
        if (vec.x > 180f)
        {
            vec.x = vec.x - 2 * 180f;
        }
        if (vec.x < -180f)
        {
            vec.x = vec.x + 2 * 180f;
        }
        if (vec.y > 180f)
        {
            vec.y = vec.y - 2 * 180f;
        }
        if (vec.y < -180f)
        {
            vec.y = vec.y + 2 * 180f;
        }
        if (vec.z > 180f)
        {
            vec.z = vec.z - 2 * 180f;
        }
        if (vec.z < -180f)
        {
            vec.z = vec.z + 2 * 180f;
        }
        return vec;
    }

    //用于对Vector3变量进行缩放操作
    //p_vecMax是该Vector3变量的最大值，p_mapMax是缩放后的最大值
    private Vector3 IntervelMapping(Vector3 p_vec, float p_vecMax, float p_mapMax)
    {
        Vector3 vec = new Vector3(p_vec.x / p_vecMax, p_vec.y / p_vecMax, p_vec.z / p_vecMax);
        vec = new Vector3(vec.x * p_mapMax, vec.y * p_mapMax, vec.z * p_mapMax);
        return vec;
    }

    //动画相机旋转功能
    public void AnimationRotate()
    {
        mHeadRotationChange = IntervelMapping(mHeadRotationChange, 180f, 10f);
        Vector3 angle = RotationTransform(mCameraEulerAngle + mHeadRotationChange);
        transform.localRotation = Quaternion.Euler(angle);
    }
}

public interface IAnimationCameraCotrollerFunc
{
    public void AnimationRotate();
}
