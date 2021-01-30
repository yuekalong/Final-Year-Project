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
   private LatLng current;

    private void Start() {

        error.text = "";
        StartCoroutine(GpsUpdate());

    }

    public void UpdateGPS()
    {   

        //StartCoroutine(GpsUpdate());
        
    }
    IEnumerator GpsUpdate(){
        while(true)
        {
            WWWForm form = new WWWForm();

            LocationFollower script= GetComponent<LocationFollower>();
            latLng=script.currentLocation; 

            form.AddField("Lat", latLng.Lat.ToString());
            form.AddField("Lng", latLng.Lng.ToString());
            //form.AddField("Lat", "1234");
            //form.AddField("Lng", "1234");

            UnityWebRequest req = UnityWebRequest.Post("http://192.168.0.155:3000/gps/location/"+PlayerPrefs.GetString("id","No ID"),form);

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

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



