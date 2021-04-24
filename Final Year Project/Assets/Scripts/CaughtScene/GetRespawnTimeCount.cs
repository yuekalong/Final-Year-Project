using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using SocketIO;
            
            

public class GetRespawnTimeCount : MonoBehaviour
{
    public TimeCountDown respawnTimer;
    private SocketManager socketManager;
    public Text timer;

    private CatchManager catchManager;
    private String caught;
    // Start is called before the first frame update
    void Start()
    {
        socketManager = FindObjectOfType<SocketManager>();

        // set the current time into time limit
        // Debug.Log(respawnTimer.TimeLeft.ToString());
        respawnTimer.StartCountDown(TimeSpan.FromMinutes(1));
        timer.text = String.Format("{0:00}:{1:00}", respawnTimer.TimeLeft.Minutes, respawnTimer.TimeLeft.Seconds);

        catchManager = FindObjectOfType<CatchManager>();
        caught="caught";

        StartCoroutine(StatusUpdate());
        StartCoroutine(CheckIfAllCaught());
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

            caught="notCaught";
            StartCoroutine(StatusUpdate());

            SceneManager.LoadScene("MapScene");
        }
    }
    IEnumerator StatusUpdate()
    {
        WWWForm form = new WWWForm();

        form.AddField("game_status", caught);

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/gps/status/"+PlayerPrefs.GetString("id","1"),form);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
            
        
    }
    IEnumerator CheckIfAllCaught()
    {
        UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/gps/locationTeammates/"+PlayerPrefs.GetString("id","1")+"/"+PlayerPrefs.GetString("group_id", "1"));

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();

        JSONNode res = JSON.Parse(req.downloadHandler.text);
        JSONNode data = res["data"];

        //2vs2 version
        if(data[0]["game_status"]=="caught" )
        {
            socketManager.sendWinLoseTeam("catch");
            socketManager.sendWinLoseOpp("catch");
        }


        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        
    }
}
