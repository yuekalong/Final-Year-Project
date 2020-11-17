using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SocketManager : MonoBehaviour
{
    SocketIOComponent socket;
    public ChatroomDisplay chatroom;
    Dictionary<string, string> user = new Dictionary<string, string>();

    // Start is called before the first frame update
    // set the socket system
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        user["id"] = PlayerPrefs.GetString("id", "No ID");
        user["name"] = PlayerPrefs.GetString("name", "No Name");

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

    #region event
    private void openEvent(SocketIOEvent ev)
    {   
        // ensure it connected the socket and have the sid
        if(socket.sid != null){
            Debug.Log("Connect Socket.io!\nSocket ID:" + socket.sid);
            user["socketID"] = socket.sid;
            // join room
            joinRoom();
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

    private void joinRoom()
    {
        socket.Emit("join-room", new JSONObject(user));
        
        Debug.Log("Room Join!");
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
        chatroom.addText(data.GetField("name").str + ": " + data.GetField("msg").str);
    }
}
