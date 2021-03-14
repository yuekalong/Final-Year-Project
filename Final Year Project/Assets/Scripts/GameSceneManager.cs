using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void GoToRegistration(){
        SceneManager.LoadScene("RegistrationScene");
    }

    public static void GoToLobby()
    {
        // TODO: care about the status, retrieve back the data if the player is still in game

        SceneManager.LoadScene("LobbyScene");
    } 

    public static void GoToWaitingRoom(){
        SceneManager.LoadScene("WaitingRoomScene");
    }

    public static void InitGame(){
        SceneManager.LoadScene("LoadingScene");
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public static void GoToMap()
    {
        SceneManager.LoadScene("MapScene");
    }

    public static void GoToChatroom()
    {
        SceneManager.LoadScene("ChatroomScene");
    }
}
