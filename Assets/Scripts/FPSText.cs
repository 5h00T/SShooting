using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSText : MonoBehaviour
{
    // 変数
    int frameCount;
    float prevTime;
    float fps;
    TextMeshProUGUI textMeshProUGUI;

    // 初期化処理
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;

        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    // 更新処理
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;
            Debug.Log(fps);
            textMeshProUGUI.text = fps.ToString();

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
