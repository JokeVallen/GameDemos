using UnityEngine;
using EasyGame;

public class SphereTrigger : ViewController, ISphereTriggerParamter, ISphereTriggerComponent, ISphereTriggerFunc, ISphereTrigger
{
    [Header("必要组件")]
    [Tooltip("触发器检测层")]
    [SerializeField] private LayerMask cTriggerLayer;
    public LayerMask TriggerLayer { get => cTriggerLayer; set => cTriggerLayer = value; }
    [Tooltip("触发器检测区域的坐标组件，也就是以这个坐标为检测区域的坐标")]
    [SerializeField] private Transform cTriggerArea;
    public Transform TriggerArea { get => cTriggerArea; set => cTriggerArea = value; }
    [Header("必要属性")]
    [Tooltip("球体的半径（触发器是球型")]
    [SerializeField] private float mRadius;
    public float Radius { get => mRadius; set => mRadius = value; }
    public bool Trigger_check { get; set; }
    public ISphereTriggerParamter IBTP { get; set; }
    public ISphereTriggerComponent IBTC { get; set; }
    public ISphereTriggerFunc IBTF { get; set; }

    /// <summary>
    /// 作用：用于检测是否进入球体触发区域
    /// <para>p_tag:待检测对象的标签</para>
    /// </summary>
    public bool TriggerCheck(string p_tag)
    {
        //如果检测区域组件为空
        if (!cTriggerArea)
        {
            Debug.Log("你的cTriggerArea为空，请挂载！");
        }
        else
        {
            Trigger_check = false;
            Collider[] colliders = Physics.OverlapSphere(cTriggerArea.position, mRadius, cTriggerLayer);
            //检测是否进入了触发区域
            foreach (var collider in colliders)
            {
                if (collider.tag == p_tag)
                {
                    Trigger_check = true;
                    break;
                }
            }
        }
        return Trigger_check;
    }
    /// <summary>
    /// 作用：绘制检测区域
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(cTriggerArea.position, mRadius);
    }
}

public interface ISphereTriggerParamter
{
    //球体半径
    public float Radius { get; set; }
    //mTrigger_check用于检测主角是否进入机关陷阱触发区域
    public bool Trigger_check { get; set; }
}

public interface ISphereTriggerComponent
{
    //区域检测层
    public LayerMask TriggerLayer { get; set; }
    //检测区域组件
    public Transform TriggerArea { get; set; }
}

public interface ISphereTriggerFunc
{
    public bool TriggerCheck(string p_tag);
}

public interface ISphereTrigger
{
    public ISphereTriggerParamter IBTP { get; set; }
    public ISphereTriggerComponent IBTC { get; set; }
    public ISphereTriggerFunc IBTF { get; set; }
}
