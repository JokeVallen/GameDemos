using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyGame;
using UnityEngine.SceneManagement;//非脚本作者添加

public class UIStartMenu : ViewController
{
    [SerializeField] Button startBtn;
    [SerializeField] Button continueBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject titlePanel;//非脚本作者添加
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("音量"))
        {
            PlayerPrefs.SetFloat("音量", 1);
        }
        GameManager.Get<EasyMusic>().SetVolume(PlayerPrefs.GetFloat("音量"));
        if (startBtn != null)//非脚本作者添加
        {
            startBtn.onClick.AddListener(() => StartTheGame());
        }
        continueBtn.onClick.AddListener(() => ContinueTheGame());
        settingBtn.onClick.AddListener(() => OpenSettingPanel());
        if (quitBtn != null)//非脚本作者添加
        {
            quitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
    private void Start()
    {
        GameManager.Get<EasyMusic>().Play("backGround", "GM001", true);
    }

    void StartTheGame()
    {
        SceneManager.LoadScene("换装");//非脚本作者添加
        Debug.Log("开始游戏");
    }

    void ContinueTheGame()
    {
        Debug.Log("继续游戏");
    }

    void OpenSettingPanel()
    {
        Debug.Log("打开设置面板");
        if (titlePanel != null)
        {
            titlePanel.SetActive(false);//非脚本作者添加
        }
        settingPanel.SetActive(true);
    }
}

