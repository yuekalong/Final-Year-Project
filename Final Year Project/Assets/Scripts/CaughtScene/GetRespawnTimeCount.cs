using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetRespawnTimeCount : MonoBehaviour
{
    public TimeCountDown respawnTimer;
    public Text timer;

    private CatchManager catchManager;

    // Start is called before the first frame update
    void Start()
    {
        // set the current time into time limit
        // Debug.Log(respawnTimer.TimeLeft.ToString());
        respawnTimer.StartCountDown(TimeSpan.FromMinutes(1));
        timer.text = String.Format("{0:00}:{1:00}", respawnTimer.TimeLeft.Minutes, respawnTimer.TimeLeft.Seconds);

        catchManager = FindObjectOfType<CatchManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(respawnTimer.TimeLeft.ToString());
        timer.text = String.Format("{0:00}:{1:00}", respawnTimer.TimeLeft.Minutes, respawnTimer.TimeLeft.Seconds);

        if (respawnTimer.TimeLeft == TimeSpan.Zero)
        {
            catchManager.Initialize();
            catchManager.StartServer();

            GameSceneManager.GoToMap();
        }
    }
}
