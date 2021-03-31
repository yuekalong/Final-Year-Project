using System.Collections;
using UnityEngine;
using UnityEngine.Networking;



public class ItemCollision : MonoBehaviour
{

    public int index =0;
    void Start()
    {

    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)" && index!=0 )
        {
            StartCoroutine(RemoveItem());  

            int num = PlayerPrefs.GetInt("num_of_bombs", 3);
            num+=1;
            if(num>5)
            {
                num=0;
            }
            PlayerPrefs.SetInt("num_of_bombs", num);

            gameObject.SetActive(false);

        }
    }
    IEnumerator RemoveItem(){

        WWWForm form = new WWWForm();

        form.AddField("index",index);
        form.AddField("game_id",PlayerPrefs.GetString("game_id","1"));

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/gps/itemRemove",form);

        yield return req.SendWebRequest();

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        
    }



}

