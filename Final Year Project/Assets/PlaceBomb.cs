using System.Collections;
using UnityEngine;
using Google.Maps.Coord;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Collections.Generic;
using Google.Maps.Unity;



namespace Google.Maps.Examples {
public class PlaceBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject button;
    private MapsService MapsService;
    private int num_bomb=0;

    public LatLng[] LatLngs = new LatLng[10];
    private LocationFollower LocationFollower;

    private GpsManager GpsManager;

    private bomb script;

    private string group_id;

    public GameObject BomePrefab;

    public GameObject OppsBombPrefab;

    private LatLng temp;

    private GameObject[] Bombs = new GameObject[20];


    void Start()
    {
        button = GameObject.Find("BombButton");
        MapsService = GetComponent<MapsService>();
        LocationFollower = GetComponent<LocationFollower>();
        GpsManager = GetComponent<GpsManager>();
        script = button.GetComponent<bomb>();

        num_bomb=script.count+1;

        group_id=PlayerPrefs.GetString("group_id","1");
        StartCoroutine(GetBomb());



    }

    // Update is called once per frame
    void Update()
    {
        if(num_bomb==script.count)
        {
            
            LatLng loc = LocationFollower.currentLocation;
            PlayerPrefs.SetString("loc_x",loc.Lat.ToString());
            PlayerPrefs.SetString("loc_y",loc.Lng.ToString());
            num_bomb+=1;
            PlayerPrefs.SetString("lock_detail", "{ id: 1, type: bomb-set }");
            SceneManager.LoadScene("PatternLock");
        }

    }
    IEnumerator GetBomb(){
        while(true)
        {
            string game_id = PlayerPrefs.GetString("game_id","1");
            UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress+"/bomb/"+game_id);

            yield return req.SendWebRequest();

            JSONNode res = JSON.Parse(req.downloadHandler.text);
            JSONNode data = res["data"];


            if(req.isNetworkError || req.isHttpError){
                Debug.LogError(req.error);

                for(int i=0;i<20;i++)
                {
                    Destroy(Bombs[0]);
                }

                yield break;
            }
            
            int count = data[0]["count"];
            
            for(int i=0;i<20;i++)
            {   
                if(i<count && group_id==data[i]["group_id"])
                {
                    Destroy(Bombs[i]);
                    Bombs[i] = GameObject.Instantiate(BomePrefab);

                    if(data[i]["bomb_id"]==2)
                    {
                        Bombs[i].transform.localScale += new Vector3(2.0f, 0.0f, 2.0f);
                    }

                    temp = new LatLng(data[i]["loc_x"],data[i]["loc_y"]);
                    Bombs[i].SetActive(true);
                    Bombs[i].AddComponent<bombCollision>();
                    Bombs[i].AddComponent<BoxCollider>();
                    Bombs[i].GetComponent<bombCollision>().check_group=group_id;
                    Bombs[i].GetComponent<bombCollision>().pattern_id=data[i]["pattern_lock_id"];
                    
                    Bombs[i].transform.position = MapsService.Coords.FromLatLngToVector3(temp);
                }
                else if(i<count && group_id!=data[i]["group_id"])
                {
                    Destroy(Bombs[i]);
                    Bombs[i] = GameObject.Instantiate(OppsBombPrefab);

                    if(data[i]["bomb_id"]==2)
                    {
                        Bombs[i].transform.localScale += new Vector3(2.0f, 0.0f, 2.0f);
                    }

                    temp = new LatLng(data[i]["loc_x"],data[i]["loc_y"]);
                    Bombs[i].SetActive(true);
                    Bombs[i].AddComponent<bombCollision>();
                    Bombs[i].AddComponent<BoxCollider>();
                    Bombs[i].GetComponent<bombCollision>().check_group=data[i]["group_id"];
                    Bombs[i].GetComponent<bombCollision>().pattern_id=data[i]["pattern_lock_id"];
                    Debug.Log(Bombs[i].GetComponent<bombCollision>().check_group);
                    Bombs[i].transform.position = MapsService.Coords.FromLatLngToVector3(temp);
                }
                else
                {
                    Destroy(Bombs[i]);
                }
                              
            }

            yield return new WaitForSeconds(5);
        }
    }

}
}
