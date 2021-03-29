using System.Collections.Generic;
using Google.Maps.Coord;
using Google.Maps.Unity;
using UnityEngine;

namespace Google.Maps.Examples {
  /// <summary>
  /// Example script for stamping down prefabs at specified lat/long coordinates. Removes other
  /// structures within a specified radius of the stamps to avoid overlap. Note that if you move
  /// the floating origin then you will need to offset the stamp prefabs, as this will not be
  /// done automatically. Stamps are spawned within a circle around a reference game object (e.g.
  /// the camera).
  /// </summary>
  public class LatLngPrefabStampingExample : MonoBehaviour {
    /// <summary>
    /// The coordinates of each stamp.
    /// </summary>
    public GameObject TEAM;
    public GameObject HINT;
    public GameObject OPP;
    public GameObject Player;
    public GameObject ITEM;

    private GameObject[] Hints = new GameObject[10];

    private GameObject[] Items = new GameObject[10];

    private GameObject[] Teammates = new GameObject[2];
    private LatLng[] TeammateLoc = new LatLng[2];
    private GameObject[] Opps = new GameObject[3];

    private LatLng[] OppsLoc = new LatLng[3];

    public List<LatLng> LatLngs;

    private int build=0;

    public int visible=0;

    
    public double[] x= new double[5];
    public double[] y= new double[5];

    public double[] hint_x= new double[10];

    public double[] hint_y= new double[10];

    public int[] hint_id= new int[10];

    public string[] hint_words= new string[10];

    /// <summary>
    /// The prefabs to use for each stamp.
    /// </summary>
    public List<GameObject> Prefabs;

    /// <summary>
    /// The radius within which to suppress other structures around each stamp.
    /// </summary>
    public List<float> SuppressionRadiuses;

    /// <summary>
    /// Object around which to spawn stamps.
    /// </summary>
    public GameObject SpawnReference;

    /// <summary>
    /// The spawning radius, measured from the spawning reference. Stamps within this radius are
    /// spawned, stamps outside are despawned.
    /// </summary>
    public float SpawnRadius = 2000.0f;

    /// <summary>
    /// A mapping from stamp indices to prefab instances.
    /// </summary>
    private Dictionary<int, GameObject> InstanceMap = new Dictionary<int, GameObject>();

    /// <summary>
    /// Reference to <see cref="MapsService"/>.
    /// </summary>
    private MapsService MapsService;
    LocationFollower script;

    /// <summary>
    /// Structures that have yet to be checked against the stamps for the need for suppression.
    /// </summary>
    private HashSet<GameObject> UncheckedStructures = new HashSet<GameObject>();

    /// <summary>
    /// Gets a reference to the attached <see cref="MapsService"/>.
    /// </summary>
    private void Awake() {
      MapsService = GetComponent<MapsService>();
    }

    /// <summary>
    /// Handles the spawning of a structure. Hides objects and adds them to a set to later be
    /// checked for the need for suppression.
    /// </summary>
    /// <param name="gameObject">The game object representing the structure.</param>
    private void HandleStructureSpawn(GameObject gameObject) {
      gameObject.AddComponent<BoxCollider>();

      // Hide immediately, so that objects that will ultimately be suppressed don't flicker onto
      // the screen before being suppressed.
      SetGameObjectVisible(gameObject, false);

      UncheckedStructures.Add(gameObject);
    }

    /// <summary>
    /// Sets up event handlers to handle structure spawning.
    /// </summary>
    private void Start() {
      script= GetComponent<LocationFollower>();
      MapsService.Events.ExtrudedStructureEvents.DidCreate.AddListener(
          args => { HandleStructureSpawn(args.GameObject); });

      MapsService.Events.ModeledStructureEvents.DidCreate.AddListener(
          args => { HandleStructureSpawn(args.GameObject); });
    }

    /// <summary>
    /// Shows or hides a game object.
    /// </summary>
    /// <param name="gameObject">The game object to show or hide.</param>
    /// <param name="show">
    /// True if the object should be shown, false if it should be hidden.
    /// </param>
    private void SetGameObjectVisible(GameObject gameObject, bool show) {
      Renderer renderer = gameObject.GetComponent<Renderer>();
      renderer.enabled = show;
    }

    /// <summary>
    /// Determines whether or not a game object represents a structure.
    /// </summary>
    /// <param name="gameObject">The game object to test.</param>
    /// <returns>True if the game object is a structure, false otherwise.</returns>
    private bool IsStructure(GameObject gameObject) {
      ExtrudedStructureComponent extrudedComponent =
          gameObject.GetComponent<ExtrudedStructureComponent>();

      ModeledStructureComponent modeledComponent =
          gameObject.GetComponent<ModeledStructureComponent>();

      return ((extrudedComponent != null) || (modeledComponent != null));
    }

    /// <summary>
    /// Suppresses other structures within the specified radius of the given stamp. Structures will
    /// only be suppressed if they have an attached collider.
    /// </summary>
    /// <param name="index">The stamp around which to suppress other structures.</param>
    private void SuppressStructuresNearStamp(int index) {
      Vector3 center = GetStampPosition(index);
      float radius = SuppressionRadiuses[index];

      Collider[] otherColliders = Physics.OverlapSphere(center, radius);

      foreach (Collider otherCollider in otherColliders) {
        GameObject other = otherCollider.gameObject;

        if (IsStructure(other)) {
          SetGameObjectVisible(other, false);
        }
      }
    }

    /// <summary>
    /// Gets the position of a stamp in Unity World Space.
    /// </summary>
    /// <param name="index">The index of the stamp whose position should be returned.</param>
    /// <returns>The position of the specified stamp in Unity World Space.</returns>
    
    private Vector3 GetStampPosition(int index) {
      LatLng latLng = LatLngs[index];

      return MapsService.Coords.FromLatLngToVector3(latLng);
    }

    /// <summary>
    /// Spawns or despawns the stamp with the specified index depending on their distance to
    /// the spawn reference object.
    /// </summary>
    /// <param name="index">The index of the stamp to spawn or despawn.</param>
    private void SpawnOrDespawnStamp(int index) {
      GameObject prefab = Prefabs[index];
      Vector3 unityCoords = GetStampPosition(index);

      float distance = Vector3.Distance(SpawnReference.transform.position, unityCoords);
      bool shouldExist = distance < SpawnRadius;
      bool exists = InstanceMap.ContainsKey(index);

      if ((shouldExist) && (!exists)) {
        GameObject spawned = GameObject.Instantiate(prefab);
        spawned.AddComponent<BoxCollider>();
        spawned.transform.position = unityCoords;
        InstanceMap[index] = spawned;
      }

      if ((!shouldExist) && (exists)) {
        Destroy(InstanceMap[index]);
        InstanceMap.Remove(index);
      }
    }

    /// <summary>
    /// Suppresses any structure that is within the suppression radius of a stamp.
    /// </summary>
    private void SuppressUncheckedStructures() {
      if (UncheckedStructures.Count == 0) {
        return;
      }

      // Unsuppresses all structures to be checked. Those that need to be suppressed will be
      // resuppressed in the same frame.
      foreach (GameObject building in UncheckedStructures) {
        SetGameObjectVisible(building, true);
      }

      for (int i = 0; i < LatLngs.Count; i++) {
        //SuppressStructuresNearStamp(i);
      }

      UncheckedStructures.Clear();
    }

    /// <summary>
    /// Performs per-frame update tasks.
    /// </summary>
    private void Update() {
       for (int i = 0; i < LatLngs.Count; i++) {
         SpawnOrDespawnStamp(i);
       }
      SuppressUncheckedStructures();
 
      playerInit();
      hintsInit();
      
      playerPosUpdate();
      hintsUpdate();

      build=1;

    }
    private void playerPosUpdate(){
      
      LatLng latLng;
      latLng=script.currentLocation; 

      TeammateLoc[0] = new LatLng(x[0],y[0]);
      TeammateLoc[1] = new LatLng(x[1],y[1]);
      Player.transform.position = MapsService.Coords.FromLatLngToVector3(latLng);
      Teammates[0].transform.position = MapsService.Coords.FromLatLngToVector3(TeammateLoc[0]);
      Teammates[1].transform.position = MapsService.Coords.FromLatLngToVector3(TeammateLoc[1]);

      if(visible==1)
      {
        OppsLoc[0] = new LatLng(x[2],y[2]);
        OppsLoc[1] = new LatLng(x[3],y[3]);
        OppsLoc[2] = new LatLng(x[4],y[4]);
        Opps[0].transform.position = MapsService.Coords.FromLatLngToVector3(OppsLoc[0]);
        Opps[1].transform.position = MapsService.Coords.FromLatLngToVector3(OppsLoc[1]);
        Opps[2].transform.position = MapsService.Coords.FromLatLngToVector3(OppsLoc[2]);
        Opps[0].SetActive(true);
        Opps[1].SetActive(true);
        Opps[2].SetActive(true);  
      }
      else
      {
        Opps[0].SetActive(false);
        Opps[1].SetActive(false);
        Opps[2].SetActive(false);  
      }


      GameObject.Find("Cam Controll").transform.position = MapsService.Coords.FromLatLngToVector3(latLng);

    }
    private void playerInit(){
      
      if(build==0)
      {
        Player = GameObject.Instantiate(TEAM);
        Player.AddComponent<BoxCollider>();

        Teammates[0] = GameObject.Instantiate(TEAM);
        Teammates[0].AddComponent<BoxCollider>();
        Teammates[1] = GameObject.Instantiate(TEAM);
        Teammates[1].AddComponent<BoxCollider>();

        Opps[0] = GameObject.Instantiate(OPP);
        Opps[1] = GameObject.Instantiate(OPP);;
        Opps[2] = GameObject.Instantiate(OPP);

        Opps[0].SetActive(false);
        Opps[1].SetActive(false);
        Opps[2].SetActive(false);  
      }

    }
    private void hintsInit(){
      
      if(build==0)
      {
        for(int i=0;i<10;i++)
        {
          Hints[i] = GameObject.Instantiate(HINT);
          Hints[i].AddComponent<BoxCollider>();
          Hints[i].AddComponent<hintCollision>();

        }
      }
    }
      private void itemsInit(){
      
      if(build==0)
      {
        for(int i=0;i<10;i++)
        {
          Items[i] = GameObject.Instantiate(ITEM);
          Items[i].AddComponent<BoxCollider>();
          Items[i].AddComponent<hintCollision>();

        }
      }
    }
    private void hintsUpdate(){

    for(int i=0;i<10;i++)
    {
      LatLng temp = new LatLng(hint_x[i],hint_y[i]);
      Hints[i].transform.position = MapsService.Coords.FromLatLngToVector3(temp);
      Hints[i].GetComponent<hintCollision>().index=hint_id[i];
      Hints[i].GetComponent<hintCollision>().words=hint_words[i];
    }

    }

  }
}
