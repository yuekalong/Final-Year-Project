﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchButton : MonoBehaviour
{
    private CatchManager catchManager;
    public Text textStatus;
    public Text connectedStatus;

    private void Awake()
    {
        catchManager = FindObjectOfType<CatchManager>();
        string groupType = PlayerPrefs.GetString("group_type", "No Group Type");

        catchManager.SetTextPlace(textStatus, connectedStatus);

        if (groupType != "protector")
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
        }
    }

    TimeCountDown catchCoolDown = new TimeCountDown();
    public void Catch()
    {
        catchManager.StartClient();

        gameObject.interactable = false;
        
        catchCoolDown.StartCountDown(TimeSpan.FromMinutes(2));
        while (catchCoolDown.TimeLeft != TimeSpan.Zero) { }

        gameObject.interactable = true;

    }
}
