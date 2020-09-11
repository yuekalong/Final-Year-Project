using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomInput : MonoBehaviour
{
    public SocketManager socket;

    public void sendMsg()
    {
        string input = GetComponent<InputField>().text;
        socket.sendMsg(input);
        GetComponent<InputField>().text = "";
    }
}
