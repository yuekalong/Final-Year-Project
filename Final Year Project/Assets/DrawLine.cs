using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private Vector2 mousePos;

    public Vector3 startPoint;
    public Vector3 tempPoint;
    Vector3 currentPoint;
    private int num=1;
    public int passed=0;
    private List<float> pattern= new List<float>();
    void Awake()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Hidden/CubeCopy"));
        line.SetWidth(0.1f, 0.1f);
        
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && num==1)
        {
            startPoint.x=tempPoint.x;
            startPoint.y=tempPoint.y;
            startPoint.z=0;
            pattern.Add(tempPoint.z);
            currentPoint=tempPoint;
            num=2;
            line.positionCount = 2; 
            line.SetPosition(0,startPoint);
            line.SetPosition(1,new Vector3(mousePos.x,mousePos.y,0));
        }
        if(Input.GetMouseButton(0) && currentPoint==tempPoint && num!=1 && passed<9)
        {
            line.SetPosition(num-1,new Vector3(mousePos.x,mousePos.y,0));
        }
        else if(Input.GetMouseButton(0) && currentPoint!=tempPoint && num!=1 && passed<9)
        {
            currentPoint=tempPoint;
            pattern.Add(tempPoint.z);
            line.SetPosition(num-1,new Vector3(tempPoint.x,tempPoint.y,0));
            line.positionCount+=1;
            num+=1;
            line.SetPosition(num-1,new Vector3(mousePos.x,mousePos.y,0));
        }
        if(passed==9)
        {
            currentPoint=tempPoint;
            pattern.Add(tempPoint.z);
            line.SetPosition(num-1,new Vector3(currentPoint.x,currentPoint.y,0));
            passed+=1;
            pattern.ForEach(Print);
        }   

    }
    void Print(float s)
    {
        Debug.Log(s);
    }


}