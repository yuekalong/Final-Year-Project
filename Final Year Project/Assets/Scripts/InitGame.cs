using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shatalmic;

public class InitGame : MonoBehaviour
{
    [SerializeField] Image circularImg;
    [SerializeField] CatchManager networking;
    [SerializeField] GameObject socket;

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
        DontDestroyOnLoad(networking);
        yield return AddProgress(0.1f);

        // get groupType
        string groupType = PlayerPrefs.GetString("group_type", "No Group Type");
        yield return AddProgress(0.1f);


        networking.Initialize();
        yield return AddProgress(0.1f);
        // if network is ready
        bool startedServer = false;
        while (!startedServer)
        {
            if (networking != null && groupType == "hunter")
            {
                networking.StartServer();
                startedServer = true;
            }
            else if (groupType == "protector")
            {
                yield return AddProgress(0.2f);
                break;
            }

            yield return AddProgress(0.05f);
        }

        // set socket
        DontDestroyOnLoad(socket);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);

        // set time limit
        TimeCountDown.StartCountDown(TimeSpan.FromMinutes(2));
        GameSceneManager.StartGame();
    }

    bool AddProgress(float value)
    {
        circularImg.fillAmount += value;
        TimeCountDown.StartCountDown(TimeSpan.FromMilliseconds(500));
        while (TimeCountDown.TimeLeft != TimeSpan.Zero) { }
        return true;
    }
}
