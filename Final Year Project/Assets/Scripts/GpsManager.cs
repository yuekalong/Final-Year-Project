using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Google.Maps.Coord;
using Google.Maps.Unity;
using SimpleJSON;

namespace Google.Maps.Examples {
public class GpsManager : MonoBehaviour
{
    public Text error;

    private LatLng latLng;
    public LatLng current;
    public Text occupation;

    private int tracker=0;
    LocationFollower script;
    private TimeCountDown gameTimer;

    LatLngPrefabStampingExample location_script;

    private void Start() {

        error.text = "";
        script= GetComponent<LocationFollower>();
        location_script= GetComponent<LatLngPrefabStampingExample>();
        gameTimer = FindObjectOfType<TimeCountDown>();

        occupation.text=PlayerPrefs.GetString("occupation","Empty");
        

        StartCoroutine(HintsGpsUpdate());
        StartCoroutine(ItemsGpsUpdate());
        StartCoroutine(TeamGpsUpdate());
        StartCoroutine(OppGpsUpdate());
        StartCoroutine(PlayerGpsUpdate());
        

    }

    void Update()
    {
        if(PlayerPrefs.GetString("occupation")=="Bomb Walker")
        {
             occupation.text="Bomb Walker :" +PlayerPrefs.GetInt("disable").ToString();
        }
    }

    IEnumerator PlayerGpsUpdate(){
        while(true)
        {
            WWWForm form = new WWWForm();

            latLng=script.currentLocation; 
            
            if(PlayerPrefs.GetString("occupation")=="Faker" && gameTimer.TimeUsed.Minutes<=15)
            {
                form.AddField("Lat", "0");
                form.AddField("Lng", "0");
            }
            else
            {
                form.AddField("Lat", latLng.Lat.ToString());
                form.AddField("Lng", latLng.Lng.ToString());
            }
            form.AddField("Visible", PlayerPrefs.GetString("visible","n"));

            UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/gps/location/"+PlayerPrefs.GetString("id","1"),form);

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            location_script.build=2;

            yield return new WaitForSeconds(7.5f);
        }
    }
    IEnumerator TeamGpsUpdate(){
        while(true)
        {

            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/gps/locationTeammates/"+PlayerPrefs.GetString("id","1")+"/"+PlayerPrefs.GetString("group_id", "1"));

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];

            location_script.x[0] = data[0]["loc_x"];
            location_script.y[0] = data[0]["loc_y"];
            location_script.x[1] = data[1]["loc_x"];
            location_script.y[1] = data[1]["loc_y"];

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            yield return new WaitForSeconds(10);
        }
    }
    IEnumerator OppGpsUpdate(){
        while(true)
        {

            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/gps/locationOpps/"+PlayerPrefs.GetString("opponent_id", "2"));

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];

            location_script.x[2] = data[0]["loc_x"];
            location_script.y[2] = data[0]["loc_y"];
            location_script.x[3] = data[1]["loc_x"];
            location_script.y[3] = data[1]["loc_y"];
            location_script.x[4] = data[2]["loc_x"];
            location_script.y[4] = data[3]["loc_y"];

            location_script.visible=0;
            if(tracker==1)
            {
                location_script.visible=1;
            }
            for(int i =0;i<3;i++)
            {
                if(data[i]["visible"]=="y")
                    location_script.visible=1;
            }

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator HintsGpsUpdate(){
        while(true)
        {
            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/gps/hints/"+PlayerPrefs.GetString("game_id", "1"));

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];

            int count=data[0]["count"];
            for(int i=0;i<10;i++)
            {
                if(i<count)
                {
                    location_script.Hints[i].SetActive(true);
                    location_script.hint_x[i] = data[i]["loc_x"];
                    location_script.hint_y[i] = data[i]["loc_y"];

                    location_script.hint_id[i] = data[i]["id"];
                    location_script.hint_words[i] = data[i]["hint_words"];
                    if(data[i]["pattern_lock_id"]==null)
                    {
                        location_script.pattern_lock_id[i] = "";
                        if(PlayerPrefs.GetString("hint_stored")=="Empty" && PlayerPrefs.GetString("occupation")=="Professor")
                            PlayerPrefs.SetString("hint_stored",location_script.hint_words[i]);
                    }
                    else
                    {
                        location_script.pattern_lock_id[i] = data[i]["pattern_lock_id"];
                    }
                    
                    location_script.Hints[i].GetComponent<hintCollision>().trigger=0;
                }
                else
                {
                    location_script.Hints[i].SetActive(false);
                    location_script.hint_x[i] = 0;
                    location_script.hint_y[i] = 0;
                    location_script.hint_words[i] = "";
                    location_script.pattern_lock_id[i] = "";
                    location_script.Hints[i].GetComponent<hintCollision>().trigger=0;
                }

            }

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            yield return new WaitForSeconds(15);
        }
    }
    IEnumerator ItemsGpsUpdate(){
        while(true)
        {
            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/gps/items/"+PlayerPrefs.GetString("game_id", "1"));

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];

            int count=data[0]["count"];

            for(int i=0;i<15;i++)
            {
                if(i<count)
                {
                    location_script.Items[i].SetActive(true);
                    location_script.item_x[i] = data[i]["loc_x"];
                    location_script.item_y[i] = data[i]["loc_y"];
                    location_script.item_id[i] = data[i]["id"];
                }
                else
                {
                    location_script.Items[i].SetActive(false);
                    location_script.item_x[i] = 0;
                    location_script.item_y[i] = 0;
                    location_script.item_id[i] = 0;
                }
                location_script.Items[i].GetComponent<ItemCollision>().trigger=0;
            }
            

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            yield return new WaitForSeconds(15);
        }
    }
    IEnumerator checkOpp(){
        tracker=1;
        yield return new WaitForSeconds(30);
        tracker=0;
    }
    public void use_tracker_skill()
    {
        StartCoroutine(checkOpp());
        PlayerPrefs.SetInt("can_track",PlayerPrefs.GetInt("can_track")-1);
    }
    
}

}



