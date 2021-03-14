using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class RegisterManager : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField confirmPassword;
    public Text error;

    // Start is called before the first frame update
    void Start()
    {
        error.text = "";
    }

    public void Register()
    {
        StartCoroutine(RegisterRequest());
    }

    IEnumerator RegisterRequest()
    {
        Debug.Log(username.text);
        Debug.Log("username.text");

        if (username.text == "" || password.text == "" || confirmPassword.text == "")
        {
            error.text = "Missing fields.";
            yield break;
        }

        if (password.text != confirmPassword.text)
        {
            error.text = "Confirm passowrd is not correct!";
            yield break;
        }

        WWWForm form = new WWWForm();

        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/auth/sign-up", form);

        // stop the function and return the state to Login(), if access this function again will start from here
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
            GameSceneManager.GoToLogin();
        }
        else
        {
            Debug.Log(res);
            // invalid credentials
            error.text = "Cannot register!";
        }
    }
}
