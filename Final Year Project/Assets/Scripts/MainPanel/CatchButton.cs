using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CatchButton : MonoBehaviour
{
    private CatchManager catchManager;
    public Text textStatus;
    public Text connectedStatus;
    public Button buttonObject;
    private TimeCountDown catchCoolDown;

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

        catchCoolDown.StartCountDown(TimeSpan.FromMinutes(0));
    }

    public void Catch()
    {
        buttonObject.interactable = false;
        catchCoolDown.StartCountDown(TimeSpan.FromSeconds(10));
        catchManager.StartClient();
    }

    void Update()
    {
        if (catchCoolDown.TimeLeft == TimeSpan.Zero)
        {
            buttonObject.interactable = true;
        }
    }
}
