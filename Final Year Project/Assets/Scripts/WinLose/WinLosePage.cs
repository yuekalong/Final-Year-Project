using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class WinLosePage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text reason;

    public Text TimeNeeded;
    private TimeCountDown gameTimer;

    
    
    void Start()
    {
        reason.text="Win!!!";
        gameTimer = FindObjectOfType<TimeCountDown>();
        string time = String.Format("{0:00}:{1:00}", gameTimer.TimeUsed.Minutes, gameTimer.TimeUsed.Seconds);
        TimeNeeded.text="Your team need "+time+" to defeat your enemy";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void change()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
