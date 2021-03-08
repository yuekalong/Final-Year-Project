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
        PlayerPrefs.SetInt("num_of_bombs", 3);
        text.GetComponent<Text>().text=PlayerPrefs.GetInt("num_of_bombs", 3).ToString();
        if(text.GetComponent<Text>().text=="0")
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    public void addbomb()
    {
        num = PlayerPrefs.GetInt("num_of_bombs", 3);

        num-=1;
        PlayerPrefs.SetInt("num_of_bombs", num);

        count+=1;
        
        text.GetComponent<Text>().text=(num).ToString();

        if(text.GetComponent<Text>().text=="0")
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
        
            
    }
}
