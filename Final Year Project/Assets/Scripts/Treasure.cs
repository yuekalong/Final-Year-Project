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

    private string type;

    bool  show=false;
    void Start()
    {
        type=PlayerPrefs.GetString("group_type", "Hunter");
        if(type!="Hunter")
        {
            gameObject.SetActive(false);
        }
    }


    public void onclick()
    {
        //inputfield.SetActive(!show);
        //show=!show;
        SceneManager.LoadScene("AR Testing");
    }
    public void submit()
    {
        //StartCoroutine(CheckCode());
    }

    /*IEnumerator CheckCode(){
    while(true)
    {
        WWWForm form = new WWWForm();
        string user_input = input.text;
        
        form.AddField("input",user_input);

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/treasure/"+PlayerPrefs.GetString("game_id", "1"),form);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();

        JSONNode res = JSON.Parse(req.downloadHandler.text);
        JSONNode data = res["data"];

        if(data==false)
        {
            input.text="Wrong Input";
        }

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }

        yield break;
    }
    }*/
}
