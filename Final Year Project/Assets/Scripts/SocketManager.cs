using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SocketManager : MonoBehaviour
{
    SocketIOComponent socket;



    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();

        if (socket != null)
        {
            // set for open message
            socket.On("open", openEvent);

            // set for error message
            socket.On("error", errorEvent);

            // set for close message
            socket.On("close", closeEvent);
        }
    }

    #region event
    private void openEvent(SocketIOEvent ev)
    {
        Debug.Log("Connect Socket.io!\nSocket ID:" + socket.sid);

        // join room
        joinRoom();
    }
    private void errorEvent(SocketIOEvent ev)
    {
        Debug.Log("Socket.io Error!\n" + ev.data);
    }
    private void closeEvent(SocketIOEvent ev)
    {
        Debug.Log("Closed Socket.io!");
    }
    #endregion

    private void joinRoom()
    {
        Debug.Log("Room Join!");

        socket.Emit("join-room");
    }

    public void sendMsg(string msg)
    {
        socket.Emit("send-msg", msg);
    }

    public void receiveMsg(string msg)
    {

    }
}
