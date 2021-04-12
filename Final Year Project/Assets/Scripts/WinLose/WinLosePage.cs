using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLosePage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text reason;
    void Start()
    {
        reason.text="Win!!!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void change()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
