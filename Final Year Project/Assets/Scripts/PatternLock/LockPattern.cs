using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LockPattern : MonoBehaviour
{
    private Dictionary<int, Circle> circles;
    private bool unlocking;

    public GameObject lineObject;
    private List<Circle> lines;
    private GameObject lineOnEdit;
    private RectTransform lineOnEditRcTs;
    private Circle circleOnEdit;

    public GameObject canvas;

    new bool enabled = true;

    // final result
    private List<int> result;

    // Start is called before the first frame update
    void Start()
    {
        /*
            lock_detail
                hint:
                {
                    id:
                    type: hint-unlock
                    hintID:
                }

                bomb:
                {
                    id:
                    type: bomb-set/bomb-unlock
                    
                    lockID:
                    (if bomb-unlock)
                }
        */
        PlayerPrefs.SetString("lock_detail", "{ id: 1, type: bomb-unlock, lockID: 515efdac-a36e-4706-89a7-8ae2776fe2f8 }");

        circles = new Dictionary<int, Circle>();
        lines = new List<Circle>();
        result = new List<int>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Circle circle = child.GetComponent<Circle>();

            circle.id = i;

            circles.Add(i, circle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!enabled){
            return;
        }

        if(unlocking){
            Vector3 mousePos = canvas.transform.InverseTransformPoint(Input.mousePosition);
            
            // refinement of the draging line
            mousePos = new Vector3(mousePos.x, mousePos.y, mousePos.z);
                
            lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x , Vector3.Distance(mousePos,circleOnEdit.transform.localPosition));
            
            // here have draging problem
            lineOnEditRcTs.rotation = Quaternion.FromToRotation(
                Vector3.up,
                (mousePos - circleOnEdit.transform.localPosition).normalized
            );
        }
    }

    GameObject CreateLine(Vector3 pos, int id){
        GameObject line = GameObject.Instantiate(lineObject, canvas.transform);
        line.transform.SetAsFirstSibling();
        
        line.transform.localPosition = new Vector3(pos.x - 25, pos.y, pos.z);
        // Debug.Log(line.transform.localPosition);
        var lineIdf = line.AddComponent<Circle>();

        lineIdf.id = id;
        
        lines.Add(lineIdf);

        return line;
    }

    void SetLine(Circle circle){
        
        // check whether the circle is connected before or not
        foreach(Circle line in lines){
            if(line.id == circle.id){
                return;
            }
        }
        // add order
        result.Add(circle.id);

        lineOnEdit = CreateLine(circle.transform.localPosition, circle.id);
        lineOnEditRcTs = lineOnEdit.GetComponent<RectTransform>();
        circleOnEdit = circle;
    }

    public void ResetLine(){
        // enabled = false;
        
        foreach(var circle in circles){
            circle.Value.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            circle.Value.GetComponent<Animator>().enabled = false;
        }

        foreach(Circle line in lines){
            Destroy(line.gameObject);
        }

        lines.Clear();

        lineOnEdit = null;
        lineOnEditRcTs = null;
        circleOnEdit = null;

        result.Clear();

        enabled = true;
    }

    void EnableColorFade(Animator anim){
        anim.enabled = true;
        anim.Rebind();
    }

    public void SubmitLine(){
        foreach (var id in result)
        {
            Debug.Log(id);
        }

        string lockType = JSON.Parse(PlayerPrefs.GetString("lock_detail"))["type"];
        Debug.Log(lockType);

        switch(lockType){
            
            case "hint-unlock":
                StartCoroutine(HintValidate());
                break;
            case "bomb-set":
                StartCoroutine(BombSet());
                break;
            case "bomb-unlock":
                StartCoroutine(BombValidate());
                break;
        }

        // result.Clear();
        // ResetLine();
    }

    IEnumerator HintValidate(){
        // generate the query path
        string queryPath = "?groupID=" + PlayerPrefs.GetString("group_id") + "&hintID=" + JSON.Parse(PlayerPrefs.GetString("lock_detail"))["hintID"] + "&input=";
        
        string inputString = "";
        
        for(int i = 0; i < result.Count; i++){

            if(i==0){
                inputString = "[" + result[i] + ",";
            }
            else if (i == result.Count - 1){
                inputString += result[i] + "]";
            }
            else{
                inputString += result[i] + ",";
            }

        }
        
        UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/hint/" + "1" + "/validate-pattern" + queryPath + inputString);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        if(res["success"]){
            Debug.Log(res["data"]);
        }
    }

    IEnumerator BombSet(){
        
        // generate the input string        
        string inputString = "";
        
        for(int i = 0; i < result.Count; i++){

            if(i==0){
                inputString = "[" + result[i] + ",";
            }
            else if (i == result.Count - 1){
                inputString += result[i] + "]";
            }
            else{
                inputString += result[i] + ",";
            }

        }
        WWWForm form = new WWWForm();
        form.AddField("gameID", "1"); // PlayerPrefs.GetString("game_id"));
        form.AddField("input", inputString);
        form.AddField("bombID", "1"); // get bomb id
        form.AddField("locX", "1"); // get loc x
        form.AddField("locY", "1"); // get loc y

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/bomb/create-bomb", form);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        if(res["success"]){
            Debug.Log("Create Successfully!");
        }
    }

    IEnumerator BombValidate(){
        
        string queryPath = "?input=";

        // generate the query path        
        string inputString = "";
        
        for(int i = 0; i < result.Count; i++){

            if(i==0){
                inputString = "[" + result[i] + ",";
            }
            else if (i == result.Count - 1){
                inputString += result[i] + "]";
            }
            else{
                inputString += result[i] + ",";
            }

        }
        
        UnityWebRequest req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/bomb/" + JSON.Parse(PlayerPrefs.GetString("lock_detail"))["lockID"]+"/validate-pattern" + queryPath+ inputString);

        // stop the function and return the state to Login(), if access this function again will start from here
        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        if(res["success"]){
            Debug.Log(res["data"]);
        }
    }

    public void OnMouseEnterCircle(Circle circle){
        // Debug.Log(circle.id);
        if(!enabled){
            return;
        }

        if(unlocking){
            lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x, Vector3.Distance(circleOnEdit.transform.localPosition, circle.transform.localPosition));
            lineOnEditRcTs.rotation = Quaternion.FromToRotation(
                Vector3.up,
                (circle.transform.localPosition - circleOnEdit.transform.localPosition).normalized
            );

            SetLine(circle);
        }

    }
    public void OnMouseExitCircle(Circle circle){
        // Debug.Log(circle.id);
        if(!enabled){
            return;
        }
    }
    public void OnMouseDownCircle(Circle circle){
        // Debug.Log(circle.id);
        if(!enabled){
            return;
        }

        unlocking = true;

        SetLine(circle);
    }
    public void OnMouseUpCircle(Circle circle){
        if(!enabled){
            return;
        }

        if(lines.Count <= 1){
            unlocking = false;
            ResetLine();
        }


        if(unlocking){

            foreach(Circle line in lines){
                // EnableColorFade(circles[line.id].gameObject.GetComponent<Animator>());
            }

            Destroy(lines[lines.Count - 1].gameObject);
            lines.RemoveAt(lines.Count - 1);

            foreach(Circle line in lines){
                EnableColorFade(line.GetComponent<Animator>());
            }
            unlocking = false;
            enabled = false;
        }
    }
    
}
