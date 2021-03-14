using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomInput : MonoBehaviour
{
    private SocketManager socketManager;

    public void sendMsg()
    {
        string input = GetComponent<InputField>().text;

        socketManager = FindObjectOfType<SocketManager>();
        socketManager.sendMsg(input);
        
        GetComponent<InputField>().text = "";
    }
}
