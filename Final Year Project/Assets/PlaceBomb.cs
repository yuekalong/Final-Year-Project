using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps.Coord;
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

    private bomb script;

    public GameObject BomePrefab;

    private GameObject[] Bombs = new GameObject[10];

    void Start()
    {
        button = GameObject.Find("Bombs");
        MapsService = GetComponent<MapsService>();
        LocationFollower = GetComponent<LocationFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        script = button.GetComponent<bomb>();
        if(num_bomb==script.count)
        {
            Bombs[num_bomb]=GameObject.Instantiate(BomePrefab);
            Bombs[num_bomb].AddComponent<BoxCollider>();
            Bombs[num_bomb].transform.position = MapsService.Coords.FromLatLngToVector3(LocationFollower.currentLocation);
            LatLngs[num_bomb] = LocationFollower.currentLocation;
            num_bomb=num_bomb+1;
        }
        if(num_bomb>0)
        {
            for(int i=0;i<num_bomb;i++)
            {
                Bombs[i].transform.position = MapsService.Coords.FromLatLngToVector3(LatLngs[i]);
            }
        }
    }
}
}
