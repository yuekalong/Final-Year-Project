using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private Vector2 mousePos;
    public Vector2 currentBall;
    public Vector2 startPoint;
    public Vector2 tempPoint;
    Vector2 currentPoint;
    Collision collision;
    int num;

    void Awake()
    {
        num = 1;
        // Create line renderer component and set its property
        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Hidden/CubeCopy"));
        line.positionCount = 2;
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.green, Color.green);
        //line.useWorldSpace = true;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && num==1)
        {
            startPoint.x=tempPoint.x;
            startPoint.y=tempPoint.y;
            currentPoint.x=tempPoint.x;
            currentPoint.y=tempPoint.y;
            num=2;
        }
        if(Input.GetMouseButtonDown(0) && num<9)
        {
            line.SetPosition(0,new Vector3(startPoint.x,startPoint.y,0));
        }
        if(Input.GetMouseButton(0) && currentPoint.x==tempPoint.x && currentPoint.y==tempPoint.y && num<=9)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(num-1,new Vector3(mousePos.x,mousePos.y,0));

        }
        else if (Input.GetMouseButton(0) && num<=9)
        {         
            line.SetPosition(num-1,new Vector3(tempPoint.x,tempPoint.y,0));
            currentPoint.x=tempPoint.x;
            currentPoint.y=tempPoint.y;
            num=num+1;
            line.positionCount = num;

        }
        if(num>9)
        {
            line.positionCount = 9;
            line.SetPosition(8,new Vector3(currentPoint.x,currentPoint.y,0));
        }

        

    }


}