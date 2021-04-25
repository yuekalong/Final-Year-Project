using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CatchButton : MonoBehaviour
{
    public CatchManager catchManager;
    public Text textStatus;
    public Text connectedStatus;
    public Button buttonObject;
    private bool scanning;

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

        scanning = false;
    }

    public void Catch()
    {
        textStatus.text = "";
        connectedStatus.text = "";
        // for start the scaning process
        catchManager.StartServer();

        StartCoroutine(Scanning());
    }

    IEnumerator Scanning()
    {
        scanning = true;
        // yield return new WaitForSeconds(60);
        yield return new WaitForSeconds(5);
        scanning = false;
    }

    void Update()
    {
        if (!scanning)
        {
            buttonObject.interactable = true;
        }
        else
        {
            buttonObject.interactable = false;
        }
    }
}
