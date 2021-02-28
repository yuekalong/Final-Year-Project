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
        if(other.gameObject.name=="MobileMaleFreeSimpleMovement1(Clone)" & words!="")
        {
            Dialog.GetComponent<HintList>().haveHint=1;
            Dialog.GetComponent<HintList>().hint_words+=words+"\n";

        }
    }



}

