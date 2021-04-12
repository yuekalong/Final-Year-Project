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
    LocationFollower script;

    LatLngPrefabStampingExample location_script;

    private void Start() {

        error.text = "";
        script= GetComponent<LocationFollower>();
        location_script= GetComponent<LatLngPrefabStampingExample>();

        StartCoroutine(HintsGpsUpdate());
        StartCoroutine(ItemsGpsUpdate());
        StartCoroutine(TeamGpsUpdate());
        StartCoroutine(OppGpsUpdate());
        StartCoroutine(PlayerGpsUpdate());
        

    }

    IEnumerator PlayerGpsUpdate(){
        while(true)
        {
            WWWForm form = new WWWForm();

            latLng=script.currentLocation; 

            form.AddField("Lat", latLng.Lat.ToString());
            form.AddField("Lng", latLng.Lng.ToString());
            form.AddField("Visible", PlayerPrefs.GetString("visible","false"));

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
            for(int i =0;i<3;i++)
            {
                if(data[i]["visible"]=="1")
                    location_script.visible=1;
            }

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);
                error.text=req.error;
                yield break;
            }
            
            yield return new WaitForSeconds(10);
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
            
            yield return new WaitForSeconds(10);
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
            
            yield return new WaitForSeconds(10);
        }
    }
    
}

}



