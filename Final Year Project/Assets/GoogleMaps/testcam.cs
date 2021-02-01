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
    float temp=120;
    private GameObject Player;
    //private Vector3 campos;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     pos= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().pos;
    //     Player= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().Player;
    //     //transform.position = new Vector3(pos.x,temp,pos.z);
    //     //campos=transform.position;
        
    //     transform.position = new Vector3(pos.x,temp,pos.z);
    //     Camera.main.transform.position=new Vector3(pos.x,temp,pos.z-80);
    //     Camera.main.transform.LookAt(pos);

    // }
    // // Update is called once per frame
    // void Update()
    // {
    //     Camera.main.transform.LookAt(pos);
    //     pos= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().pos;;
    //     //transform.position = new Vector3(pos.x,temp,pos.z);
    //     if(Input.touchCount ==1 && Input.GetTouch (0).phase == TouchPhase.Moved)
    //     {
    //         Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
    //         Player.transform.Rotate (0,-touchDeltaPosition.x*speed,0);
            
    //     }
    //     if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
    //     {
    //         Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    //         transform.Rotate (0,-touchDeltaPosition.x*speed,0);
    //         temp+=-touchDeltaPosition.y * speed;
    //         //transform.position= new Vector3(0,temp,0);
    //     }
        
    //     //campos=transform.position;
    //     //Camera.main.transform.position = new Vector3(campos.x,temp,campos.z-80);
    // }


    void Start(){
        // pos= GameObject.Find("WorldContainer").GetComponent<LatLngPrefabStampingExample>().pos;
        // pos= GetComponent<LocationFollower>().pos; 
        pos= GameObject.Find("MobileMaleFreeSimpleMovement1(Clone)").transform.position; 
        transform.position = new Vector3(pos.x,temp,pos.z);
        //Camera.main.transform.position=new Vector3(pos.x,temp,pos.z-120);
        // Camera.main.transform.LookAt(pos);
    }

    void Update(){
        pos= GameObject.Find("MobileMaleFreeSimpleMovement1(Clone)").transform.position; 
        transform.position = new Vector3(pos.x,temp,pos.z-temp*0.1f);
        
        Camera.main.transform.LookAt(pos);
        if(Input.touchCount ==1 && Input.GetTouch (0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Rotate (0,-touchDeltaPosition.x*speed,0);      
        }
        else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            //transform.Rotate (0,-touchDeltaPosition.x*speed,0);
            if(temp>20 && temp<300)
            {
                temp+=-touchDeltaPosition.y * speed;
            }
            if(temp<20)
            {
                temp=22;
            }
            if(temp>350)
            {
                temp=348;
            }
            
        }
    }
}
                                                