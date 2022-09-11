using UnityEngine;
using EasyGame;
using UnityEngine.SceneManagement;

//换装提示
public class DressTipActive : ViewController
{
    private RectTransform cRectTransform;
    private bool isFinish;

    private void Update()
    {
        if (!isFinish)
        {
            UpdateFunc();
            isFinish = true;
            this.enabled = false;
        }
    }
    private void UpdateFunc()
    {
        Self.SendCommand<CmdShowDialogPanel>(new DialogInfo("Tips", "换装提示", DialogueStyle.Tip));
        cRectTransform = GameObject.FindGameObjectWithTag("Tip").GetComponent<RectTransform>();
        cRectTransform.anchoredPosition3D = Vector3.zero;
        cRectTransform.localScale = Vector3.one;
        cRectTransform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}

