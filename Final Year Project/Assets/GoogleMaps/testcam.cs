using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcam : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount ==1 && Input.GetTouch (0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Translate (-touchDeltaPosition.x*speed,0,-touchDeltaPosition.y*speed);
        }
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(0,-touchDeltaPosition.y * speed,0);
        }
    }
}
