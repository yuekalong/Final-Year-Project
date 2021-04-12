using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AR_Checker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Goback());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene("WinLose");
    }

    IEnumerator Goback(){
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MapScene");
    }
}
