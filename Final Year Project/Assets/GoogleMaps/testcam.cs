using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Examples ;
using Google.Maps.Unity;
using Google.Maps.Coord;

public class testcam : MonoBehaviour
{
    public float speed;
    private Vector3 pos;
    float temp=150;

    //private Vector3 campos;
    
    // Start is called before the first frame update
    void Start()
    {
        pos= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().pos;
        //transform.position = new Vector3(pos.x,temp,pos.z);
        //campos=transform.position;
        Camera.main.transform.LookAt(pos);
        transform.position = new Vector3(0,temp,0);

    }
    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.LookAt(pos);
        pos= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().pos;;
        //transform.position = new Vector3(pos.x,temp,pos.z);
        if(Input.touchCount ==1 && Input.GetTouch (0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Rotate (0,-touchDeltaPosition.x*speed,0);
            
        }
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            //transform.Rotate (0,-touchDeltaPosition.x*speed,0);
            temp+=-touchDeltaPosition.y * speed;
            transform.position = new Vector3(0,temp,0);
        }
        
        //campos=transform.position;
        //Camera.main.transform.position = new Vector3(campos.x,temp,campos.z-80);
    }
}
                                                