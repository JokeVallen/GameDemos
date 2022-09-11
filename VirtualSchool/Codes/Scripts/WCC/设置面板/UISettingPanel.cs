using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyGame;
using TMPro;

public class UISettingPanel : ViewController
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Toggle windowedToggle;
    [SerializeField] TMP_Dropdown ResolutionDropdown;
    [SerializeField] Button backBtn;
    private void Awake()
    {
        //InitDropdown();
        InitPanel();
        musicSlider.onValueChanged.AddListener(volume => ChangeVolume(volume));
        windowedToggle.onValueChanged.AddListener(isOpening => SetWindowing(isOpening));
        ResolutionDropdown.onValueChanged.AddListener(index => SetResolution(index));
        backBtn.onClick.AddListener(() => BackToStartMenu());
    }

    void ChangeVolume(float volume)
    {
        GameManager.Get<EasyMusic>().SetVolume(volume);
    }

    void SetWindowing(bool isOpening)
    {
        if (isOpening)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    void BackToStartMenu()
    {
        //保存设置数据
        PlayerPrefs.SetFloat("音量", musicSlider.value);
        gameObject.SetActive(false);
    }

    void InitDropdown()
    {
        var index = 0;
        //获取设置当前屏幕分辩率
        Resolution[] resolutions = Screen.resolutions;
        List<string> size = new List<string>();
        for (int i = resolutions.Length - 1; i > 0; i--)
        {
            if (resolutions[i].width < 800) break;

            var width = resolutions[i].width;
            var height = resolutions[i].height;
            var result = width + "*" + height;
            if (size.Contains(result)) continue;
            size.Add(width + "*" + height);
        }
        for (int i = 0; i < size.Count; i++)
        {
            if (Screen.width + "*" + Screen.height == size[i])
            {
                index = i;
            }
        }
        ResolutionDropdown.AddOptions(size);
        ResolutionDropdown.value = index;
    }

    void SetResolution(int index)
    {
        var options = ResolutionDropdown.options;
        var strs = options[index].text.Split('*');
        var width = int.Parse(strs[0]);
        var height = int.Parse(strs[1]);
        Screen.SetResolution(width, height, !windowedToggle.isOn);
    }

    void InitPanel()
    {
        InitDropdown();
        windowedToggle.isOn = !Screen.fullScreen;
        musicSlider.value = GameManager.Get<EasyMusic>().Volume;
    }
}

