using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shatalmic;
using System;

public class CatchManager : MonoBehaviour
{
    private Networking networking = null;
    private List<Networking.NetworkDevice> connectedDeviceList = null;

    // private Networking.NetworkDevice deviceToSkip = null;
    private bool isServer;

    private String serverName;
    private String clientName;

    public Text TextStatus;
    public Text ConnectedText;

    private String groupType;

    public int respawnTime;

    private void Start()
    {   
        TextStatus.text = "";
        ConnectedText.text = "";
        groupType = PlayerPrefs.GetString("group_type", "No Group Type");

        if (networking == null)
        {
            networking = GetComponent<Networking>();
            networking.Initialize((error) =>
            {
                BluetoothLEHardwareInterface.Log("Error: " + error);
            }, (message) =>
            {
                // it update the status of the connection
                if (TextStatus != null)
                    TextStatus.text = message;

                BluetoothLEHardwareInterface.Log("Message: " + message);
            });
        }
    }

    public void Update(){
        // if network is ready
        if(networking != null && groupType == "hunter" && !isServer){
            StartServer();
        }
    }

    public void StartServer()
    {   
        isServer = true;

        serverName = PlayerPrefs.GetString("group_id", "No Group ID");

        networking.StartServer(serverName, (connectedDevice) => // onDeviceReady
                    {   
                        // device connected
                        if (connectedDeviceList == null)
                            connectedDeviceList = new List<Networking.NetworkDevice>();

                        if (!connectedDeviceList.Contains(connectedDevice))
                        {
                            connectedDeviceList.Add(connectedDevice);
                            TextStatus.color = Color.red;
                        }
                        
                        clientName = connectedDevice.Name;

                        ConnectedText.text = "Server Connected! Client Name: " + clientName;
                        SendSignal("Client Connected!");

                    }, (disconnectedDevice) => // onDeviceDisconnected
                    {   
                        // device disconnected
                        if (connectedDeviceList != null && connectedDeviceList.Contains(disconnectedDevice))
                            connectedDeviceList.Remove(disconnectedDevice);
                        
                        StopServer();

                    }, (dataDevice, characteristic, bytes) => // onDeviceData
                    {
                        // client data received
                        ConnectedText.text = System.Text.Encoding.ASCII.GetString(bytes);

                        if(ConnectedText.text == "disconnect"){
                            StopServer();
                        }
                    });
    }

    public void StopServer()
    {
        networking.StopServer(()=>{});

        StartCoroutine(StartServerAgain());
    }

    public IEnumerator StartServerAgain()
    {
        yield return new WaitForSeconds(respawnTime);

        StartServer();
    }

    public void StartClient()
    {
        isServer = false;

        serverName = PlayerPrefs.GetString("opponent_id", "No Opponent ID");
        clientName = PlayerPrefs.GetString("name", "No Name");

        networking.StartClient(serverName, clientName, () => // onStartedAdvertising
                    {   
                        // when finding server
                        networking.StatusMessage = "Started advertising";
                    }, (clientName, characteristic, bytes) => // onCharacteristicWritten
                    {   
                        // receive server data
                        ConnectedText.text = System.Text.Encoding.ASCII.GetString(bytes);
                        StopClient();
                    });
        
    }

    public void StopClient()
    {
        networking.StopClient(()=>{
            ConnectedText.text = "Client Disconnected!";       
        });
        SendSignal("disconnect");
    }

    public void SendSignal(String signal){
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(signal);

        if(isServer){
            networking.WriteDevice(connectedDeviceList[0], bytes, ()=>{});
        }
        else{
            networking.SendFromClient(bytes);
        }
    }
}