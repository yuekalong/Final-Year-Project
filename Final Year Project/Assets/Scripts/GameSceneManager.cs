using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void startGame()
    {
        string input = GetComponent<InputField>().text;
        PlayerPrefs.SetString("name", input);
        Debug.Log(PlayerPrefs.GetString("name", "No Name"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
