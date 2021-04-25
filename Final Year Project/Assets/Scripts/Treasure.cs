using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;


public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject inputfield;

    public InputField input;

    public Button but;

    private string type;

    bool  show=false;
    void Start()
    {
        if(PlayerPrefs.GetInt("scanned",0)==1)
        {
            but.interactable=false;
            StartCoroutine(CountDown());
        }

    }


    public void onclick() 
    {
        //inputfield.SetActive(!show);
        //show=!show;
        SceneManager.LoadScene("AR Testing");
        PlayerPrefs.SetInt("scanned",1);
    }

    IEnumerator CountDown(){
        yield return new WaitForSeconds(20);
        PlayerPrefs.SetInt("scanned",0);
        but.interactable=true;
    }
}
