using UnityEngine.UI;
using EasyGame;

public class GameDebug : ViewController
{
    public static GameDebug st_Gdb = new GameDebug();
    private Text cDebug;

    private void Awake()
    {
        cDebug = GetComponent<Text>();
    }

    public void SetLog(string textinfo)
    {
        cDebug.text = textinfo;
    }
}

