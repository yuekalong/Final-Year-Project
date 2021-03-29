using System.Collections;
using UnityEngine;
using UnityEngine.Networking;



public class hintCollision : MonoBehaviour
{

    public int index;

    public string words="";

    private GameObject Dialog;


    void Start()
    {
        Dialog=GameObject.Find("ListButton");
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)" & words!="" )
        {
            Dialog.GetComponent<HintList>().haveHint=1;
            string temp=PlayerPrefs.GetString("hint_stored");
            temp+=(words+"\n");
            PlayerPrefs.SetString("hint_stored",temp);
            StartCoroutine(RemoveHint());  
            gameObject.SetActive(false);

        }
    }
    IEnumerator RemoveHint(){

        WWWForm form = new WWWForm();

        form.AddField("index",index);
        form.AddField("game_id",PlayerPrefs.GetString("game_id","1"));

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/gps/hintRemove",form);

        yield return req.SendWebRequest();

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        
    }



}

