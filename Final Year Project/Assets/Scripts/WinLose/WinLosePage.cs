using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace SocketIO
{
public class WinLosePage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text reason;

    private CatchManager catchManager;
    private SocketIOComponent networking;
    private Text TimeNeeded;
    private TimeCountDown gameTimer;

    
    
    void Start()
    {
        gameTimer = FindObjectOfType<TimeCountDown>();
        networking = FindObjectOfType<SocketIOComponent>();
        catchManager = FindObjectOfType<CatchManager>();

        string time = String.Format("{0:00}:{1:00}", gameTimer.TimeUsed.Minutes, gameTimer.TimeUsed.Seconds);
        
        String receievedReason = PlayerPrefs.GetString("reason");
        if(receievedReason=="unseal")
        {
            if(PlayerPrefs.GetString("group_type")=="hunter")
            {
                reason.text="Your Team Unseal the treasure!";
                GameObject.Find("LoseTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team need "+time+" to defeat your enemy";
            }
            else
            {
                reason.text="Hunter Unseal the treasure...";
                GameObject.Find("WinTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team defeated your enemy in "+time+" .";
            }
        }
        if(receievedReason=="catch")
        {
            if(PlayerPrefs.GetString("group_type")=="hunter")
            {
                reason.text="All Hunters Get Caught...";
                GameObject.Find("WinTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team defeated your enemy in "+time+" .";
            }
            else
            {
                reason.text="All Hunters Get Caught!!!";
                GameObject.Find("LoseTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team need "+time+" to defeat your enemy";
            }
        }
        if(receievedReason=="time_up")
        {
            if(PlayerPrefs.GetString("group_type")=="hunter")
            {
                reason.text="Time up!";
                GameObject.Find("WinTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team defeated in "+time+" .";
            }
            else
            {
                reason.text="Time up!";
                GameObject.Find("LoseTextContainer").SetActive(false);
                TimeNeeded=GameObject.Find("TimeNeeded").GetComponent<Text>();
                TimeNeeded.text="Your team need "+time+" to defeat your enemy";
            }
        }
        Destroy(gameTimer);
        Destroy(networking);
        Destroy(catchManager);
        Destroy(GameObject.Find("BluetoothLEReceiver"));
        PlayerPrefs.DeleteAll();

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
}