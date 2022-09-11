using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class ViewDressUp : ViewController
{
    [SerializeField] GameObject boy;
    [SerializeField] GameObject girl;
    private void Awake()
    {
        var dressUpSys = GameManager.Get<SysDressUp>();
        dressUpSys.CurRole.OnValueChanged = sex => RoleChange(sex);
        Self.EventRegister<EventDressUp>(e => e.Excute(this.transform.GetComponentsInChildren<Transform>()[0]));
    }

    #region ÇÐ»»½ÇÉ«
    void RoleChange(string sex)
    {
        if (sex == "girl")
        {
            boy.SetActive(false);
            girl.SetActive(true);
            girl.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            boy.SetActive(true);
            girl.SetActive(false);
            boy.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
    }
    #endregion


}

