using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Coord;
using Google.Maps.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;


public class bombCollision : MonoBehaviour
{
    private string group_id;

    public string check_group;
    public string pattern_id;
    void Start()
    {
        group_id = PlayerPrefs.GetString("group_id","1");
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide");
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)" && group_id!=check_group)
        {
            Debug.Log("collide ok");
            PlayerPrefs.SetString("lock_detail", "{ id: 1, type: bomb-unlock, lockID: "+pattern_id+" }");
            PlayerPrefs.SetString("visible","y");
            StartCoroutine(PlayerGpsUpdate());
            gameObject.SetActive(false);
            SceneManager.LoadScene("PatternLock");
            
        }
    }
    IEnumerator PlayerGpsUpdate(){
        while(true)
        {
            WWWForm form = new WWWForm();

            form.AddField("Visible", PlayerPrefs.GetString("visible","n"));

            UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/gps/location/"+PlayerPrefs.GetString("id","1"),form);

            // stop the function and return the state to Login(), if access this function again will start from here
            yield return req.SendWebRequest();

            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);

                yield break;
            }
            

        }
    }




}

