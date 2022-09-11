using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class ViewSpinWithMouse : ViewController
{
    bool isClick;
    Vector3 newPos;
    Vector3 oldPos;
    private void OnMouseDown()
    {
        isClick = true;
    }

    private void OnMouseUp()
    {
        isClick = false;
    }

    private void Update()
    {
        if (!isClick) return;

        newPos = Input.mousePosition;

        var offset = newPos - oldPos;
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y) && Mathf.Abs(offset.x) > 5)
        {
            if (offset.x > 0)
                transform.Rotate(Vector3.up, -offset.x / offset.x * 5);
            else
                transform.Rotate(Vector3.up, offset.x / offset.x * 5);
        }

        oldPos = Input.mousePosition;

    }
}

