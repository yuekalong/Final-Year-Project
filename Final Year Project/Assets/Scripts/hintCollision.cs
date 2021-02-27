using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Google.Maps.Coord;
using Google.Maps.Unity;
using UnityEngine.SceneManagement;


public class hintCollision : MonoBehaviour
{

    public int index;
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
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)")
        {
            
        }
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("Nothing", "");

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Got Hint");
            }
        }
    }


}

