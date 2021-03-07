using System.Collections;
using UnityEngine;
using UnityEngine.Networking;



public class hintCollision : MonoBehaviour
{

    public int index;

    public int array_id;
    public string words="";

    private GameObject Dialog;

    private int disable=0;
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
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)" & words!="" & disable==0)
        {
            Dialog.GetComponent<HintList>().haveHint=1;
            Dialog.GetComponent<HintList>().hint_words+=(words+"\n");
            gameObject.SetActive(false);
            disable=1;
            StartCoroutine(RemoveHint());  

        }
    }
    IEnumerator RemoveHint(){

        WWWForm form = new WWWForm();

        form.AddField("index",index);

        UnityWebRequest req = UnityWebRequest.Post("http://192.168.0.155:3000/gps/hintRemove",form);

        yield return req.SendWebRequest();

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        
    }



}

