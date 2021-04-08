using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetGameTimeCount : MonoBehaviour
{
    private TimeCountDown gameTimer;
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        // set the current time into time limit
        // Debug.Log(gameTimer.TimeLeft.ToString());

        gameTimer = GameObject.FindGameObjectsWithTag("GameTimer")[0].GetComponent<TimeCountDown>();

        timer.text = String.Format("{0:00}:{1:00}", gameTimer.TimeLeft.Minutes, gameTimer.TimeLeft.Seconds);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(gameTimer.TimeLeft.ToString());
        timer.text = String.Format("{0:00}:{1:00}", gameTimer.TimeLeft.Minutes, gameTimer.TimeLeft.Seconds);

        if(gameTimer.TimeLeft == TimeSpan.Zero){
            Debug.Log("Game End!");
        }
    }
}
