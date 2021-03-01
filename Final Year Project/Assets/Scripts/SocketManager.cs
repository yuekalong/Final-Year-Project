using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

public class SocketManager : MonoBehaviour
{
    SocketIOComponent socket;
    private Text chatroomDisplay;
    private ScrollRect scrollRect;
    Dictionary<string, string> user = new Dictionary<string, string>();

    public int unseenMsg;

    private string sceneName;

    // Start is called before the first frame update
    // set the socket system
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        user["id"] = PlayerPrefs.GetString("id", "No ID");
        user["name"] = PlayerPrefs.GetString("name", "No Name");
        sceneName = SceneManager.GetActiveScene().name;
        
        if (socket != null)
        {
            // set for open message
            socket.On("open", openEvent);

            // set for error message
            socket.On("error", errorEvent);

            // set for close message
            socket.On("close", closeEvent);

            socket.On("receive-msg", receiveMsg);
        }
    }

    public void SetChatroomDisplay(Text inputChatroomDisplay, ScrollRect inputScrollRect)
    {
        chatroomDisplay = inputChatroomDisplay;
        scrollRect = inputScrollRect;
    }

    #region event
    private void openEvent(SocketIOEvent ev)
    {
        // ensure it connected the socket and have the sid
        if (socket.sid != null)
        {
            Debug.Log("Connect Socket.io!\nSocket ID:" + socket.sid);
            
            Debug.Log(sceneName);

            if(sceneName == "LoadingScene"){
                user["socketID"] = socket.sid;

                // assume no close of game
                unseenMsg = 0;
                
                // join chatroom
                joinChatoom();
            }
            else if (sceneName == "WaitingRoomScene"){
                joinWaitingRoom();
            }
        }
    }
    private void errorEvent(SocketIOEvent ev)
    {
        Debug.Log("Socket.io Error!\n");
        Debug.Log(ev);
    }
    private void closeEvent(SocketIOEvent ev)
    {
        Debug.Log("Closed Socket.io!");
    }
    #endregion

    private void joinChatoom()
    {
        socket.Emit("join-chatroom", new JSONObject(user));

        Debug.Log("Chatroom Join!");
    }

    private void joinWaitingRoom()
    {
        socket.Emit("join-waiting-room", new JSONObject(user));

        Debug.Log("Waiting Room Join!");
    }

    public void sendMsg(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = user["id"];
        data["socketID"] = user["socketID"];
        data["name"] = user["name"];
        data["msg"] = msg;
        Debug.Log("Send Message: " + msg);

        socket.Emit("send-msg", new JSONObject(data));
    }

    public void receiveMsg(SocketIOEvent ev)
    {
        Debug.Log("Receive Message: " + ev.data);
        JSONObject data = ev.data;

        if (SceneManager.GetActiveScene().name != "ChatroomScene")
        {

            // if not in chatroom scene
            unseenMsg++;
        }
        else
        {

            // if in chatroom scene
            if (chatroomDisplay.text != "")
            {
                // content.text = content.text + "\n" + msg;
                chatroomDisplay.text = chatroomDisplay.text + "\n" + data.GetField("name").str + ": " + data.GetField("msg").str;
            }
            else
            {
                chatroomDisplay.text = data.GetField("name").str + ": " + data.GetField("msg").str;
            }

            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }

    public void GetHistory()
    {
        socket.Emit("get-history", new JSONObject(user));
        unseenMsg = 0;
    }
}
