using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Examples ;
using Google.Maps.Unity;
using Google.Maps.Coord;

public class testcam : MonoBehaviour
{
    public float speed;
    LocationFollower script;
    float temp=150;
    
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<LocationFollower>();
        transform.position = new Vector3(script.pos.x,temp,script.pos.z);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(script.pos.x,temp,script.pos.z);
        if(Input.touchCount ==1 && Input.GetTouch (0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Rotate (0,-touchDeltaPosition.x*speed,0);
        }
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            if(temp >= 100 && temp <=200)
            {
                temp+=-touchDeltaPosition.y * speed;
            }
            if(temp < 100)
            {
                transform.position = new Vector3(script.pos.x,100,script.pos.z);
            }
            if(temp > 200)
            {
                transform.position = new Vector3(script.pos.x,200,script.pos.z);
            }
            
        }
    }
}
