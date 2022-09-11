using System.Collections;
using UnityEngine;
using EasyGame;
using TMPro;
using System;

public class UIDialogPanel : ViewController
{
    [SerializeField] Transform context;
    TMP_Text dialogText;
    bool isDialog;
    string[] texts;
    int index = 1;
    bool typing;
    bool skipping;
    Action onComplete;
    private void Awake()
    {
        dialogText = context.GetComponent<TMP_Text>();
    }

    public void StartTypewriter(string[] texts, Action onComplete)
    {
        this.texts = texts;
        isDialog = true;
        this.onComplete = onComplete;
        StartCoroutine(Typewriter(dialogText, texts[0]));
    }

    private void Update()
    {
        if (!isDialog) return;

        if (Input.GetMouseButtonDown(0) && !typing)
        {
            skipping = false;
            if (index == texts.Length)
            {
                onComplete?.Invoke();
                GameObject.Destroy(this.gameObject);
                return;
            }
            StartCoroutine(Typewriter(dialogText, texts[index]));
            ++index;
        }
        else if (Input.GetMouseButtonDown(0) && typing)
        {
            skipping = true;
        }
    }

    private IEnumerator Typewriter(TMP_Text dailogText, string text)
    {
        dailogText.text = "";
        typing = true;
        for (int i = 0; i < text.Length; i++)
        {
            if (skipping)
            {
                dailogText.text = text.Replace('-', '\n');
                break;
            }
            dailogText.text += text[i].Equals('-') ? '\n' : text[i];
            yield return new WaitForSeconds(0.1f);
        }
        typing = false;
        yield return null;
    }
}

