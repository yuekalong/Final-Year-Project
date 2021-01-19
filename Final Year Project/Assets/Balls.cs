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

        obj.GetComponent<DrawLine>().tempPoint.x=transform.position.x;
        obj.GetComponent<DrawLine>().tempPoint.y=transform.position.y;
        obj.GetComponent<DrawLine>().tempPoint.z=transform.position.z;
        //Debug.Log(transform.position);
        if(gameObject.GetComponent<Renderer>().material.color!=Color.red)
        {
            gameObject.GetComponent<Renderer>().material.color=Color.red;
            obj.GetComponent<DrawLine>().passed+=1;
        }
        

        //If your mouse hovers over the GameObject with the script attached, output this message

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
