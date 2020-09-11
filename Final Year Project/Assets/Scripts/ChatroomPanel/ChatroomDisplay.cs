using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomDisplay : MonoBehaviour
{
    Text content;
    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Text>();
        content.text = "";
    }

    public void addText(string msg)
    {
        if (content.text != "")
        {
            content.text = content.text + "\n" + msg;
        }
        else
        {
            content.text = msg;
        }
    }
}
