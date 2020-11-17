﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class AuthManager : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text error;

    private void Start() {
        error.text = "";
    }

    public void Login()
    {   
        error.text = "";
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest(){
        WWWForm form = new WWWForm();

        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest req = UnityWebRequest.Post("http://localhost:3000/auth/login", form);
        
        yield return req.SendWebRequest();
        
        JSONNode res = JSON.Parse(req.downloadHandler.text);

        if(res["success"]){
            JSONNode userInfo = res["data"]["userInfo"];

            PlayerPrefs.SetString("id", userInfo["id"]);
            PlayerPrefs.SetString("name", userInfo["name"]);
            PlayerPrefs.SetString("game_status", userInfo["game_status"]);
            PlayerPrefs.SetString("jwt", res["data"]["token"]);

            Debug.Log("ID: " + PlayerPrefs.GetString("id", "No ID"));
            Debug.Log("Name: " + PlayerPrefs.GetString("name", "No Name"));
            Debug.Log("Game Status: " + PlayerPrefs.GetString("game_status", "No Game Status"));
            Debug.Log("JWT: " + PlayerPrefs.GetString("jwt", "No JWT"));

            GameSceneManager.startGame();
        }
        else{
            // invalid credentials
            error.text="invalid credentials";
        }
    }
}
