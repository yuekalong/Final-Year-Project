using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

public class SocketManager : MonoBehaviour
{
    SocketIOComponent socket;
    Dictionary<string, string> user = new Dictionary<string, string>();
    private string sceneName;

    // Start is called before the first frame update
    // set the socket system
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        user["id"] = PlayerPrefs.GetString("id", "No ID");
        user["game_id"] = PlayerPrefs.GetString("game_id", "No Game ID");
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


            // chatroom
            socket.On("receive-msg", receiveMsg);

            // waiting room
            socket.On("player-count", updatePlayerCount);
        }
    }

    #region socketEvent
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

    #region waitingRoomEvent
    private Text waitingRoomDisplay;

    private void joinWaitingRoom()
    {
        socket.Emit("join-waiting-room", new JSONObject(user));

        Debug.Log("Waiting Room Join!");
    }

    public void SetWaitingRoomDisplay(Text inputWaitingRoomDisplay)
    {
        waitingRoomDisplay = inputWaitingRoomDisplay;
    }

    public void updatePlayerCount(SocketIOEvent ev)
    {
        Debug.Log("Receive Message: " + ev.data);
        JSONObject data = ev.data;

        waitingRoomDisplay.text = data["count"] + " / " + data["maximum_count"];
    }

    public void GetCurrentWaitingCount(){
        socket.Emit("get-current-player-count", new JSONObject(user));
    }
    #endregion

    #region chatroomEvent
    private Text chatroomDisplay;
    private ScrollRect scrollRect;
    public int unseenMsg;

    private void joinChatoom()
    {
        socket.Emit("join-chatroom", new JSONObject(user));

        Debug.Log("Chatroom Join!");
    }

    public void SetChatroomDisplay(Text inputChatroomDisplay, ScrollRect inputScrollRect)
    {
        chatroomDisplay = inputChatroomDisplay;
        scrollRect = inputScrollRect;
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
    #endregion
}
