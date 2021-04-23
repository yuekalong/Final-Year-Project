using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class AR_Checker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Goback());
        StartCoroutine(SeleteTreasure());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        if("hunter"==PlayerPrefs.GetString("group_type", "empty"))
        {
            PlayerPrefs.SetString("reason","unseal");
            SceneManager.LoadScene("WinLose");
        }
            
    }

    IEnumerator Goback(){
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MapScene");
    }
    IEnumerator SeleteTreasure(){
        while(true)
        {
            WWWForm form = new WWWForm();


            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/treasure/"+PlayerPrefs.GetString("game_id", "1"));

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];

            for(int i=1;i<=3;i++)
            {
                if(i.ToString()!=data["treasure_id"])
                {
                    GameObject temp=GameObject.Find(i.ToString());
                    temp.SetActive(false);
                }
            }


            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                yield break;
            }

            yield break;
        }
    }
}
