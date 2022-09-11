using UnityEngine;
using EasyGame;

public class BoxTrigger : ViewController, IBoxTriggerParamter, IBoxTriggerComponent, IBoxTriggerFunc, IBoxTrigger
{
    [Header("必要组件")]
    [Tooltip("触发器检测层")]
    [SerializeField] private LayerMask cTriggerLayer;
    public LayerMask TriggerLayer { get => cTriggerLayer; set => cTriggerLayer = value; }
    [Tooltip("触发器检测区域的坐标组件，也就是以这个坐标为检测区域的坐标")]
    [SerializeField] private Transform cTriggerArea;
    public Transform TriggerArea { get => cTriggerArea; set => cTriggerArea = value; }
    [Header("必要属性")]
    [Tooltip("触发器检测区域的长宽高（触发器是盒型）")]
    [SerializeField] private Vector3 mTrapTriggerSize;
    public Vector3 TrapTriggerSize { get => mTrapTriggerSize; set => mTrapTriggerSize = value; }
    public bool Trigger_check { get; set; }
    public IBoxTriggerParamter IBTP { get; set; }
    public IBoxTriggerComponent IBTC { get; set; }
    public IBoxTriggerFunc IBTF { get; set; }

    /// <summary>
    /// 作用：用于检测是否进入盒型触发区域
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
            Collider[] colliders = Physics.OverlapBox(TriggerArea.position, TrapTriggerSize, Quaternion.identity, cTriggerLayer);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(TriggerArea.position, TrapTriggerSize);
    }
}

public interface IBoxTriggerParamter
{
    //检测区域的半径
    public Vector3 TrapTriggerSize { get; set; }
    //mTrigger_check用于检测主角是否进入触发区域
    public bool Trigger_check { get; set; }
}

public interface IBoxTriggerComponent
{
    //区域检测层
    public LayerMask TriggerLayer { get; set; }
    //检测区域组件
    public Transform TriggerArea { get; set; }
}

public interface IBoxTriggerFunc
{
    bool TriggerCheck(string p_tag);
}

public interface IBoxTrigger
{
    public IBoxTriggerParamter IBTP { get; set; }
    public IBoxTriggerComponent IBTC { get; set; }
    public IBoxTriggerFunc IBTF { get; set; }
}