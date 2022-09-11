using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIDressUp : ViewController
{
    [SerializeField] Button okBtn;
    private void Awake()
    {
        okBtn.onClick.AddListener(() =>
        {
            GameManager.Get<EasyMusic>().PauseAll();//�ǽű��������
            GameManager.Get<EasyMusic>().Play("backGround", "GM002", true);//�ǽű��������
            //�л�����Ϸ����
            SceneManager.LoadScene("SchoolSceneDay");//�ǽű��������
        });
    }
    public void ChangeRole(string sex)
    {
        var dressSys = GameManager.Get<SysDressUp>();
        dressSys.CurRole.Value = sex;
    }

    public void ChangeDressUp(string partAndIndex)
    {
        var dressSys = GameManager.Get<SysDressUp>();
        var str = partAndIndex.Split('-');
        dressSys.ChangeDressUp(str[0], int.Parse(str[1]));
    }

    public void PlayAnimation(string animaName)
    {
        var animation = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
        if (!animation.IsPlaying(animaName))
        {
            animation.Play(animaName);
            animation.PlayQueued("idle1");
        }
    }
}