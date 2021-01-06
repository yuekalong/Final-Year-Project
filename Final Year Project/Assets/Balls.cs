using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    GameObject obj;
    void Awake()
    {
        obj = GameObject.Find("GameObject");
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        obj.GetComponent<DrawLine>().tempPoint.x=transform.position.x;
        obj.GetComponent<DrawLine>().tempPoint.y=transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
