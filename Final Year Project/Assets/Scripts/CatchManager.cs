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

    private Networking.NetworkDevice deviceToSkip = null;

    public String NetworkName;
    public String ClientName;

    public Text TextStatus;

    private void Start()
    {
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
        // StartServer();
    }

    public void StartServer()
    {
        TextStatus.text = "";

        networking.StartServer(NetworkName, (connectedDevice) =>
                    {
                        if (connectedDeviceList == null)
                            connectedDeviceList = new List<Networking.NetworkDevice>();

                        if (!connectedDeviceList.Contains(connectedDevice))
                        {
                            connectedDeviceList.Add(connectedDevice);
                            TextStatus.color = Color.red;
                        }
                    }, (disconnectedDevice) =>
                    {
                        if (connectedDeviceList != null && connectedDeviceList.Contains(disconnectedDevice))
                            connectedDeviceList.Remove(disconnectedDevice);
                    }, (dataDevice, characteristic, bytes) =>
                    {
                        deviceToSkip = dataDevice;
                        // ParseCubePosition(bytes);
                    });
    }

    public void StartConnect()
    {
        TextStatus.text = "";

        networking.StartClient(NetworkName, ClientName, () =>
                    {
                        networking.StatusMessage = "Started advertising";
                    }, (clientName, characteristic, bytes) =>
                    {
                        // if (Cube != null)
                        //     Cube.SetActive(true);
                        // ParseCubePosition(bytes);
                    });
    }
}