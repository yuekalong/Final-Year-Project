using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void startGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public static void goToChatroom()
    {
        SceneManager.LoadScene("ChatroomScene");
    }

    public static void goToMap()
    {
        SceneManager.LoadScene("MapScene");
    }
}
