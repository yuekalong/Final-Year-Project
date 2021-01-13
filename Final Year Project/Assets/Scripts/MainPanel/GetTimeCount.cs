using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetTimeCount : MonoBehaviour
{
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        // set the current time into time limit
        Debug.Log(TimeCountDown.TimeLeft.ToString());
        timer.text = TimeCountDown.TimeLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(TimeCountDown.TimeLeft.ToString());
        timer.text = TimeCountDown.TimeLeft.ToString();
    }
}
