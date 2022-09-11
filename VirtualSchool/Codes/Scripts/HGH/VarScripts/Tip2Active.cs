using UnityEngine;
using EasyGame;

//角色控制提示
public class Tip2Active : ViewController
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
        {
            DestroyImmediate(gameObject);
        }
    }
}

