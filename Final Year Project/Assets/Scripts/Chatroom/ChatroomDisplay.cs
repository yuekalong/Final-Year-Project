using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomDisplay : MonoBehaviour
{
    private SocketManager socketManager;
    public Text content;
    public ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        content.text = "";

        socketManager = FindObjectOfType<SocketManager>();
        socketManager.SetChatroomDisplay(content, scrollRect);

        socketManager.GetHistory();
    }

}
