using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class AuthManager : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text error;

    private void Start() {
        error.text = "";
    }

    public void Login()
    {   
        error.text = "";

        // to start the IEnumerator
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest(){
        WWWForm form = new WWWForm();

        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest req = UnityWebRequest.Post("http://192.168.8.118:3000/auth/login", form);
        
        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            error.text=req.error;
            yield break;
        }

        if(res["success"]){
            JSONNode userInfo = res["data"]["userInfo"];
            PlayerPrefs.SetString("id", userInfo["id"]);
            PlayerPrefs.SetString("name", userInfo["name"]);
            PlayerPrefs.SetString("status", userInfo["game_status"]);
            PlayerPrefs.SetString("jwt", res["data"]["token"]);

            Debug.Log("ID: " + PlayerPrefs.GetString("id", "No ID"));
            Debug.Log("Name: " + PlayerPrefs.GetString("name", "No Name"));
            Debug.Log("Game Status: " + PlayerPrefs.GetString("status", "No Game Status"));
            Debug.Log("JWT: " + PlayerPrefs.GetString("jwt", "No JWT"));

            if(userInfo["game_status"].Equals("playing")){
                Debug.Log("GetBasicGameInfo");
                // StartCoroutine(GetBasicGameInfo());
                GameSceneManager.GoToLobby();
            }
            else GameSceneManager.GoToLobby();
                   
        }
        else{
            Debug.Log(res);
            // invalid credentials
            error.text="invalid credentials";
        }
    }

    IEnumerator GetBasicGameInfo(){
        Debug.Log(PlayerPrefs.GetString("id", "No ID"));

        UnityWebRequest req = UnityWebRequest.Get("http://192.168.8.118:3000/account/game-basic-info/"+ PlayerPrefs.GetString("id", "No ID"));
        
        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);

        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            error.text=req.error;
            yield break;
        }
        if(res["success"]){
            JSONNode data = res["data"];

            PlayerPrefs.SetString("game_id", data["game"]["id"]);
            PlayerPrefs.SetString("area_id", data["game"]["area"]);
            PlayerPrefs.SetString("treasure_id", data["game"]["treasure"]);
            PlayerPrefs.SetString("group_id", data["group"]["id"]);
            PlayerPrefs.SetString("group_type", data["group"]["type"]);
            PlayerPrefs.SetString("opponent_id", data["opponent"]["id"]);

            Debug.Log("Game ID: " + PlayerPrefs.GetString("game_id", "No Game ID"));
            Debug.Log("Area ID: " + PlayerPrefs.GetString("area_id", "No Area ID"));
            Debug.Log("Treasure ID: " + PlayerPrefs.GetString("treasure_id", "No Treasure ID"));
            Debug.Log("Group ID: " + PlayerPrefs.GetString("group_id", "No Group ID"));
            Debug.Log("Group Type: " + PlayerPrefs.GetString("group_type", "No Group Type"));
            Debug.Log("Opponent ID: " + PlayerPrefs.GetString("opponent_id", "No Opponent ID"));

            GameSceneManager.GoToLobby();
        }
        else{
            Debug.Log(res);
        }
    }
}
