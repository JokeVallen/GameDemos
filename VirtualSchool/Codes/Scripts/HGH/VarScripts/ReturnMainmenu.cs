using EasyGame;
using UnityEngine.SceneManagement;

public class ReturnMainmenu : ViewController
{
    public void ReturnMenu()
    {
        SceneManager.LoadScene("开始菜单");
    }
}

