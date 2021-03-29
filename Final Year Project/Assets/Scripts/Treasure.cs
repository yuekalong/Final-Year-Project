using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Google.Maps.Coord;
using Google.Maps.Unity;
using SimpleJSON;


public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject inputfield;

    public GameObject input;

    bool  show=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(input.GetComponent<Text>().text);
    }

    public void onclick()
    {
        inputfield.SetActive(!show);
        show=!show;
    }
    public void submit()
    {
        StartCoroutine(CheckCode());
    }

    IEnumerator CheckCode(){
    while(true)
    {
        WWWForm form = new WWWForm();

        form.AddField("input",input.GetComponent<Text>().text);

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/treasure/"+PlayerPrefs.GetString("game_id", "1"),form);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();

        JSONNode res = JSON.Parse(req.downloadHandler.text);
        JSONNode data = res["data"];

        if(data==false)
        {
            input.GetComponent<Text>().text="Wrong Input";
        }

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }

        yield break;
    }
    }
}
