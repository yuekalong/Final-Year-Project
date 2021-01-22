using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomDisplay : MonoBehaviour
{
    private SocketManager socketManager;
    Text content;
    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Text>();
        content.text = "";

        socketManager = FindObjectOfType<SocketManager>();
        socketManager.SetChatroomDisplay(content);

        socketManager.GetHistory();
    }

}
