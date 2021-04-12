using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class CreateRoomManager : MonoBehaviour
{
    public Dropdown dropdown;
    public Text error;

    void Start()
    {
        error.text = "";
    }

    public void OnClick()
    {
        StartCoroutine(CreateRoom());
    }

    IEnumerator CreateRoom()
    {
        Debug.Log(dropdown.value + 1);

        WWWForm form = new WWWForm();

        form.AddField("mapNumber", dropdown.value + 1);

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/lobby/create-room", form);

        yield return req.SendWebRequest();

        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
            error.text = req.error;
            yield break;
        }

        if (res["success"])
        {
            GameSceneManager.GoToLobby();
        }
        else
        {
            Debug.Log(res);
            // invalid credentials
            error.text = "Cannot create!";
        }
    }
}
