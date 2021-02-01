using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Coord;
using Google.Maps.Unity;
using UnityEngine.SceneManagement;


public class bombCollision : MonoBehaviour
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
            SceneManager.LoadScene("PatternLock");
        }
    }


}

