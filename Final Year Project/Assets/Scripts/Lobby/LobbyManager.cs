using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LobbyManager : MonoBehaviour
{
    public GameObject roomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRooms());
    }

    public void ClickRefresh()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine(GetRooms());
    }

    IEnumerator GetRooms()
    {
        UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/lobby");

        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
            yield break;
        }
        if (res["success"])
        {
            Debug.Log(res["data"]);

            // int roomNo = 1;

            foreach (var room in res["data"])
            {
                GameObject roomObject = GameObject.Instantiate(roomPrefab, gameObject.transform);
                roomObject.transform.SetAsFirstSibling();

                Room roomIdf = roomObject.GetComponent<Room>();


                roomIdf.id = room.Value["game_id"];
                roomIdf.content.text = "Room: " + room.Value["game_id"] + "\nArea: " + GetMapName(room.Value["map_number"]) + "\nPlayer: " + room.Value["player_count"] + " / 6";

                // roomNo++;
            }
        }
    }

    public string GetMapName(string mapNumber)
    {
        switch (mapNumber)
        {
            case "1":
                return "CUHK Base";
            case "2":
                return "CUHK Main Campus";
            case "3":
                return "CUHK Peak";
            default:
                return "";
        }
    }
}
