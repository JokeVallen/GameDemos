using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddUIRoot
{
    [MenuItem("EasyGame/创建UIRoot", false, 8)]
    static void Create()
    {
        var UIRoot = new GameObject("UIRoot");
        var canvas = new GameObject("Canvas");
        var eventSystem = new GameObject("EventSystem");
        eventSystem.transform.SetParent(UIRoot.transform);
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var canvasScaler = canvas.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        canvasScaler.matchWidthOrHeight = 0.5f;
        canvas.AddComponent<GraphicRaycaster>();
        canvas.AddComponent<MainCanvas>();
        canvas.transform.SetParent(UIRoot.transform);
    }
}

