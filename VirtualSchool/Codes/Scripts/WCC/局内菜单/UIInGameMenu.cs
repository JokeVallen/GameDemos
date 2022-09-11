using UnityEngine;
using UnityEngine.UI;
using EasyGame;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIInGameMenu : ViewController
{
    [SerializeField] Button ContinueBtn;
    [SerializeField] Button SettingBtn;
    [SerializeField] Button BackMainMenuBtn;
    [SerializeField] MenuInGameActive MenuInGame;//非脚本作者添加
    IMIGAFunc IMIGA;//非脚本作者添加
    GameObject settingPanel;
    RectTransform rect;
    private void Awake()
    {
        IMIGA = MenuInGame as IMIGAFunc;//非脚本作者添加
        GetSettingPanel();
        rect = GetComponent<RectTransform>();
        ContinueBtn.onClick.AddListener(() => Continue());
        SettingBtn.onClick.AddListener(() => settingPanel.gameObject.SetActive(true));
        BackMainMenuBtn.onClick.AddListener(() => BackToStartMenu());
    }

    //Mark：HGH
    void GetSettingPanel()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == "设置")
            {
                settingPanel = transform.parent.GetChild(i).gameObject;
                break;
            }
        }
    }
    void Continue()
    {
        IMIGA.SetIsUse(true);//非脚本作者添加
        //rect.DOAnchorPosY(Screen.height, 1);
    }

    void BackToStartMenu()
    {
        SceneManager.LoadScene("开始菜单");
    }
}

