using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    GameObject obj;
    int start=0;
    void Awake()
    {
        obj = GameObject.Find("GameObject");
        
    }
    void OnMouseOver()
    {
        if(start==0)
        {
            obj.GetComponent<DrawLine>().tempPoint.x=transform.position.x;
            obj.GetComponent<DrawLine>().tempPoint.y=transform.position.y;
            obj.GetComponent<DrawLine>().tempPoint.z=transform.position.z;
            start=1;
        }
        if(gameObject.GetComponent<Renderer>().material.color!=Color.red && start==1)
        {
            Debug.Log("Mouse is over GameObject.");
            obj.GetComponent<DrawLine>().tempPoint.x=transform.position.x;
            obj.GetComponent<DrawLine>().tempPoint.y=transform.position.y;
            obj.GetComponent<DrawLine>().tempPoint.z=transform.position.z;
            gameObject.GetComponent<Renderer>().material.color=Color.red;
        }
        //If your mouse hovers over the GameObject with the script attached, output this message

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
