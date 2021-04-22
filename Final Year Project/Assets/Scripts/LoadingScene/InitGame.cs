using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shatalmic;
using UnityEngine.Networking;
using SimpleJSON;


public class InitGame : MonoBehaviour
{
    [SerializeField] Image circularImg;
    [SerializeField] CatchManager networking;
    [SerializeField] GameObject socket;
    [SerializeField] TimeCountDown gameTimer;

    [SerializeField] [Range(0, 1)] float progress = 0f;

    private string[] occupation ={"Bomb Walker","Enhancer","Tracker","Faker","Professor","Terrorist","Robber","Avengers"};


    bool isStarted = false;

    void Awake()
    {
        circularImg.fillAmount = progress;
        int rand  = UnityEngine.Random.Range(1, 9);
        PlayerPrefs.SetString("occupation",occupation[rand]);
        if(PlayerPrefs.GetString("occupation")=="Bomb Walker")
        {
            PlayerPrefs.SetInt("disable_bome",2);
        }
        if(PlayerPrefs.GetString("occupation")=="Tracker")
        {
            PlayerPrefs.SetInt("can_track",3);
        }
            
    }

    void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(Initialize());
            isStarted = true;
        }
    }

    IEnumerator Initialize()
    {
        // get player details
        yield return StartCoroutine(GetBasicGameInfo());

        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);
        yield return AddProgress(0.1f);

        // get the player type (hunter or protector)
        // set BLE
        DontDestroyOnLoad(networking);
        yield return AddProgress(0.1f);

        // get groupType
        string groupType = PlayerPrefs.GetString("group_type", "No Group Type");
        yield return AddProgress(0.1f);


        networking.Initialize();
        Debug.Log("Start initialize BLE");

        yield return AddProgress(0.1f);

        // if network is ready
        bool startedServer = false;
        // bluetooth initialization <= comment if want to work on PC
        while (!startedServer)
        {
            if (networking != null && groupType == "hunter")
            {
                networking.StartServer();
                startedServer = true;
            }
            else if (networking != null && groupType == "protector")
            {
                yield return AddProgress(0.2f);
                break;
            }

            yield return AddProgress(0.05f);
        }
        // ----------------------------------------

        // set socket
        DontDestroyOnLoad(socket);

        yield return AddProgress(0.1f);

        PlayerPrefs.SetString("hint_stored", "Empty");
        yield return AddProgress(0.1f);

        // set time limit
        gameTimer.StartCountDown(TimeSpan.FromMinutes(2));
        DontDestroyOnLoad(gameTimer);

        GameSceneManager.StartGame();
    }


    TimeCountDown progressTimer = new TimeCountDown();
    bool AddProgress(float value)
    {
        circularImg.fillAmount += value;
        progressTimer.StartCountDown(TimeSpan.FromMilliseconds(500));
        while (progressTimer.TimeLeft != TimeSpan.Zero) { }
        return true;
    }


    IEnumerator GetBasicGameInfo()
    {
        UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/account/game-basic-info/" + PlayerPrefs.GetString("game_id", "No ID") + "/" + PlayerPrefs.GetString("id", "No ID"));

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
            yield break;
        }
        if (res["success"])
        {
            JSONNode data = res["data"];

            PlayerPrefs.SetString("game_id", data["game"]["id"]);
            PlayerPrefs.SetString("map", data["game"]["map_number"]);
            PlayerPrefs.SetString("treasure_id", data["game"]["treasure"]);
            PlayerPrefs.SetString("group_id", data["group"]["id"]);
            PlayerPrefs.SetString("group_type", data["group"]["type"]);
            PlayerPrefs.SetString("opponent_id", data["opponent"]["id"]);
            PlayerPrefs.SetString("visible","n");
            PlayerPrefs.SetInt("num_of_bombs", 0);
            if(PlayerPrefs.GetString("occupation")=="Terrorist")
                PlayerPrefs.SetInt("num_of_bombs", 2);

            Debug.Log("Game ID: " + PlayerPrefs.GetString("game_id", "No Game ID"));
            Debug.Log("Area ID: " + PlayerPrefs.GetString("area_id", "No Area ID"));
            Debug.Log("Treasure ID: " + PlayerPrefs.GetString("treasure_id", "No Treasure ID"));
            Debug.Log("Group ID: " + PlayerPrefs.GetString("group_id", "No Group ID"));
            Debug.Log("Group Type: " + PlayerPrefs.GetString("group_type", "No Group Type"));
            Debug.Log("Opponent ID: " + PlayerPrefs.GetString("opponent_id", "No Opponent ID"));
            Debug.Log("Number of Bombs: " + PlayerPrefs.GetString("num_of_bombs", "No num_of_bombs"));
        }
        else
        {
            Debug.Log(res);
        }
    }
}
