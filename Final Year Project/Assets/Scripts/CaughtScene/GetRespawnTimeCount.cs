using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetRespawnTimeCount : MonoBehaviour
{
    public TimeCountDown respawnTimer;
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        // set the current time into time limit
        // Debug.Log(respawnTimer.TimeLeft.ToString());
        respawnTimer.StartCountDown(TimeSpan.FromMinutes(1));
        timer.text = String.Format("{0:00}:{1:00}", respawnTimer.TimeLeft.Minutes, respawnTimer.TimeLeft.Seconds);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(respawnTimer.TimeLeft.ToString());
        timer.text = String.Format("{0:00}:{1:00}", respawnTimer.TimeLeft.Minutes, respawnTimer.TimeLeft.Seconds);

        if (respawnTimer.TimeLeft == TimeSpan.Zero)
        {
            GameSceneManager.GoToMap();
        }
    }
}
