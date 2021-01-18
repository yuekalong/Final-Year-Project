using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void GoToLobby()
    {
        string status = PlayerPrefs.GetString("status", "No Game Status");

        if(status == "waiting"){
            Debug.Log("Go To Lobby!");
        }
        else if(status == "playing"){
            // StartGame();
            InitGame();
        }
    } 

    public static void InitGame(){
        SceneManager.LoadScene("LoadingScene");
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("MapScene");
        // SceneManager.LoadScene("Playground");
    }

    public static void GoToChatroom()
    {
        SceneManager.LoadScene("ChatroomScene");
    }

    public static void GoToMap()
    {
        SceneManager.LoadScene("MapScene");
    }
}
