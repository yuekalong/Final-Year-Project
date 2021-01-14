using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
    [SerializeField] Image circularImg;
    [SerializeField] [Range(0, 1)] float progress = 0f;

    bool isStarted = false;

    void Awake()
    {
        circularImg.fillAmount = progress;
    }

    void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(Initialize());
            isStarted = true;
        }
    }

    IEnumerator Initialize()
    {
        // get the player type (hunter or protector)
        // set BLE
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);

        // set socket
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);

        // set time limit
        GameSceneManager.StartGame();
        TimeCountDown.StartCountDown(TimeSpan.FromMinutes(2));
    }

    bool AddProgress(float value)
    {
        TimeCountDown.StartCountDown(TimeSpan.FromMilliseconds(500));
        while (TimeCountDown.TimeLeft != TimeSpan.Zero)
        {
            circularImg.fillAmount = (float)(value * (500 - TimeCountDown.TimeLeft.TotalMilliseconds) / 500);
        }
        return true;
    }
}
