using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public int count=-1;
    void Start()
    {
        count=-1;
    }
    public void addbomb()
    {
        count+=1;
    }
}
