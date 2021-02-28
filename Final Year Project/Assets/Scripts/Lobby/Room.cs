using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class Room : MonoBehaviour
{
    public string id;
    public Text content;

    public void OnButtonClick()
    {
        StartCoroutine(JoinRoom());
    }

    IEnumerator JoinRoom()
    {
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("gameID", id); // PlayerPrefs.GetString("game_id"));
        form.AddField("userID", "2");// PlayerPrefs.GetString("id"));

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/lobby/join", form);

        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
            Debug.Log("You already in that room!");
            yield break;
        }
        if (res["success"])
        {
            Debug.Log("Join Successfully!");
        }
    }
}
