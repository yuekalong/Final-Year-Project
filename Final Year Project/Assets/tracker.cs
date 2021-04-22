using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Google.Maps.Coord;
using Google.Maps.Unity;
namespace Google.Maps.Examples {
public class tracker : MonoBehaviour
{
    // Start is called before the first frame update
    public Text number;

    public Button but;

    public GameObject script;
    void Start()
    {
        if(PlayerPrefs.GetString("occupation")=="Tracker")
        {
            gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        number.text=PlayerPrefs.GetInt("can_track").ToString();
        if(number.text=="0")
        {
            but.interactable=false;
        }
    }
    public void track_opp()
    {
        script.GetComponent<GpsManager>().use_tracker_skill();
    }
}
}