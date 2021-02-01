using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bomb : MonoBehaviour
{
    public int count=-1;
    private int num;

    private GameObject text;
    void Start()
    {
        count=-1;
        text = GameObject.Find("Number");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(text.GetComponent<Text>().text=="0")
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
            
    }
    public void addbomb()
    {
        num = int.Parse(text.GetComponent<Text>().text);
        count+=1;
        text.GetComponent<Text>().text=(num-1).ToString();

            
    }
}
