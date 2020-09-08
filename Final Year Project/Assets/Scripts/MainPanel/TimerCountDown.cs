using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    public Text timer;
    public float timeLimit = 10f;

    public float current = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // set the current time into time limit
        current = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        // minus 1 with respect with the time frame
        current -= 1 * Time.deltaTime;

        // if the current is negative (minor bug), fix it
        if (current <= 0)
        {
            current = 0f;
        }
        else
        {
            // set for display
            float minute = Mathf.Floor(current / 60f);
            float sec = Mathf.Floor(current % 60f);
            timer.text = minute.ToString("00") + ":" + sec.ToString("00");
        }
    }
}
