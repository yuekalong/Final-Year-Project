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
        text.GetComponent<Text>().text=(3).ToString();
        PlayerPrefs.SetInt("num_of_bombs", 3);
        num = PlayerPrefs.GetInt("num_of_bombs", 3);
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
        Debug.Log(num);
        if(num!=0)
        {   num-=1;
            PlayerPrefs.SetInt("num_of_bombs", num);
            count+=1;
            text.GetComponent<Text>().text=(num).ToString();
        }
        

            
    }
}
