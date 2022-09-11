using UnityEngine;
using EasyGame;

public class CmdShowDialogPanel : Command
{
    public override void Excute(ViewController source, object data)
    {
        var info = (DialogInfo)data;
        var dailogText = ShowDialogPanel(info.Style);
        var texts = GetDialogText(info.Name, info.DialogName).Split('\n');

        if (texts.Length == 0) return;
        dailogText.StartTypewriter(texts, info.OnComplete);
    }

    UIDialogPanel ShowDialogPanel(DialogueStyle style)
    {
        var modDialog = GameManager.Get<ModDialog>();
        var mainCanvas = MainCanvas.Transform;
        GameObject dialogPanel = null;
        if (style == DialogueStyle.General)
            dialogPanel = GameObject.Instantiate(modDialog.DialogPanel);
        else
            dialogPanel = GameObject.Instantiate(modDialog.TipPanel);
        dialogPanel.transform.SetParent(mainCanvas);
        var rectTransform = dialogPanel.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.sizeDelta = Vector3.zero;
        var dailogText = dialogPanel.GetComponent<UIDialogPanel>();

        return dailogText;
    }
    string GetDialogText(string name, string dialogName)
    {
        var modDialog = GameManager.Get<ModDialog>();
        foreach (var item in modDialog.DataList[name].dialogList)
        {
            if (item.DialogName == dialogName)
            {
                return item.dialogText.text;
            }
        }
        return null;
    }
}
