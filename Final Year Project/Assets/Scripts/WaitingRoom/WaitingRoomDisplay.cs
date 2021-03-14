using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomDisplay : MonoBehaviour
{
    private SocketManager socketManager;
    public Text content;

    // Start is called before the first frame update
    void Start()
    {
        content.text = "";

        socketManager = FindObjectOfType<SocketManager>();
        socketManager.SetWaitingRoomDisplay(content);

        socketManager.GetCurrentWaitingCount();
    }
}
