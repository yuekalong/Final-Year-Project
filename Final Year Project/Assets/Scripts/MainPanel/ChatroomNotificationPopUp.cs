using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomNotificationPopUp : MonoBehaviour
{
    private SocketManager socketManager;
    public Text msgNo;

    // Start is called before the first frame update
    void Start()
    {
        socketManager = FindObjectOfType<SocketManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (socketManager.unseenMsg > 0)
        {
            msgNo.text = socketManager.unseenMsg.ToString();
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
