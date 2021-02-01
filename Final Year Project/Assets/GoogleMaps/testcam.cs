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
    private Vector2 previoustDistance;


    void Start(){
        pos= GameObject.Find("MobileMaleFreeSimpleMovement1(Clone)").transform.position; 
        transform.position = new Vector3(pos.x,temp,pos.z);

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
        else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Vector2 firstpre = Input.GetTouch (0).position-Input.GetTouch (0).deltaPosition;
            Vector2 secondpre = Input.GetTouch (1).position-Input.GetTouch (1).deltaPosition;

            Vector2 preDistance =firstpre-secondpre;
            Vector2 cureDistance =Input.GetTouch (0).position-Input.GetTouch (1).position;

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            if(temp>20 && temp<350 )
            {
                temp+=(preDistance.magnitude-cureDistance.magnitude) * speed ;
            }
            if(temp<20)
            {
                temp=22;
            }
            if(temp>350)
            {
                temp=345;
            }

            
        }
    }

}
                                                