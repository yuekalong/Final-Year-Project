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

        sceneName = SceneManager.GetActiveScene().name;

        if (socket != null)
        {
            // set for open message
            socket.On("open", OpenEvent);

            // set for error message
            socket.On("error", ErrorEvent);

            // set for close message
            socket.On("close", CloseEvent);


            // chatroom
            socket.On("receive-msg", ReceiveMsg);

            //whole game
            socket.On("receive-reason", ReceiveReason);

            // waiting room
            socket.On("player-count", UpdatePlayerCount);
            socket.On("start-game", InitGame);
        }
    }

    public void SetSocketUser()
    {
        // set ids
        user["group_id"] = PlayerPrefs.GetString("group_id", "No Group ID");
        user["opponent_id"] = PlayerPrefs.GetString("opponent_id", "No Opponent ID");
        user["name"] = PlayerPrefs.GetString("name", "No Name");

        Debug.Log("User ID:" + user["id"]);
        Debug.Log("Game ID:" + user["game_id"]);
        Debug.Log("Group ID:" + user["group_id"]);
        Debug.Log("Opponent ID:" + user["opponent_id"]);
        Debug.Log("Name:" + user["name"]);
    }

    #region socketEvent
    private void OpenEvent(SocketIOEvent ev)
    {
        // ensure it connected the socket and have the sid
        if (socket.sid != null)
        {
            Debug.Log("Connect Socket.io!\nSocket ID:" + socket.sid);

            Debug.Log(sceneName);

            if (sceneName == "LoadingScene")
            {
                user["socketID"] = socket.sid;

                // assume no close of game
                unseenMsg = 0;

                // join chatroom
                // joinChatoom();
            }
            else if (sceneName == "WaitingRoomScene")
            {
                joinWaitingRoom();
            }
        }
    }
    private void ErrorEvent(SocketIOEvent ev)
    {
        Debug.Log("Socket.io Error!\n");
        Debug.Log(ev);
    }
    private void CloseEvent(SocketIOEvent ev)
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

    public void UpdatePlayerCount(SocketIOEvent ev)
    {
        Debug.Log("Receive Message: " + ev.data);
        JSONObject data = ev.data;

        waitingRoomDisplay.text = data["count"] + " / " + data["maximum_count"];
    }

    public void GetCurrentWaitingCount()
    {
        socket.Emit("get-current-player-count", new JSONObject(user));
    }

    public void InitGame(SocketIOEvent ev)
    {
        GameSceneManager.InitGame();
    }
    #endregion

    #region chatroomEvent
    private Text chatroomDisplay;
    private ScrollRect scrollRect;
    public int unseenMsg;

    public void joinChatoom()
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
        data["name"] = user["name"];
        data["group_id"] = user["group_id"];
        data["msg"] = msg;
        Debug.Log("Send Message: " + msg);

        socket.Emit("send-msg", new JSONObject(data));
    }

    public void sendWinLoseTeam(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = user["id"];
        data["name"] = user["name"];
        data["group_id"] = user["group_id"];
        data["msg"] = msg;

        socket.Emit("send-winlose-team", new JSONObject(data));
    }

    public void sendWinLoseOpp(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = user["id"];
        data["name"] = user["name"];
        data["opponent_id"] = user["opponent_id"];
        data["msg"] = msg;

        socket.Emit("send-winlose-opp", new JSONObject(data));
    }

    public void ReceiveMsg(SocketIOEvent ev)
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
    public void ReceiveReason(SocketIOEvent ev)
    {
        Debug.Log("Receive Message: " + ev.data);
        JSONObject data = ev.data;

        PlayerPrefs.SetString("reason", data.GetField("msg").str);
        SceneManager.LoadScene("WinLose");


    }

    public void GetHistory()
    {
        socket.Emit("get-history", new JSONObject(user));
        unseenMsg = 0;
    }
    #endregion
}
