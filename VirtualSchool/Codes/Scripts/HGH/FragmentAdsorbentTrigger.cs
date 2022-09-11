using UnityEngine;
using EasyGame;

//拼图碎片是否进入吸附区域的检测器
public class FragmentAdsorbentTrigger : ViewController, IFragmentAdsorbentTriggerFunc
{
    private bool isEnter;

    public bool TriggerCheck()
    {
        return isEnter;
    }

    public void SetIsEnter(bool enter)
    {
        isEnter = enter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fragment")
        {
            isEnter = true;
        }
        else
        {
            isEnter = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fragment")
        {
            isEnter = false;
        }
    }

}

public interface IFragmentAdsorbentTriggerFunc
{
    public bool TriggerCheck();
    public void SetIsEnter(bool enter);
}

