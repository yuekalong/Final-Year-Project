using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

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


    // line
    private Color lineColor = Color.cyan;
    private int siblingIdx = 0;

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
        // PlayerPrefs.SetString("lock_detail", "{ id: 1, type: hint-unlock, hintID: 1 }");
        //PlayerPrefs.SetString("lock_detail", "{ id: 1, type: bomb-set }");
        //PlayerPrefs.SetString("lock_detail", "{ id: 1, type: bomb-unlock, lockID: 1589e619-931d-47ca-8296-cbda34af080f }");

        circles = new Dictionary<int, Circle>();
        lines = new List<Circle>();
        result = new List<int>();
        lineColor.a = 0.85f;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Circle circle = child.GetComponent<Circle>();

            circle.id = i;

            circles.Add(i, circle);
        }

        StartCoroutine(ShowPattern());
    }

    
    IEnumerator ShowPattern(){
        // Debug.Log(JSON.Parse(PlayerPrefs.GetString("lock_detail"))["hintID"]);

        UnityWebRequest req = new UnityWebRequest();
        string type = JSON.Parse(PlayerPrefs.GetString("lock_detail"))["type"];
        switch(type){
            case "hint-unlock":
                req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/hint/" + "1" + "/pattern-lock/" + JSON.Parse(PlayerPrefs.GetString("lock_detail"))["hintID"]);
                break;
            case "bomb-set":
                yield break;
            case "bomb-unlock":
                req = UnityWebRequest.Get(PlatformDefines.apiAddress + "/bomb/" + JSON.Parse(PlayerPrefs.GetString("lock_detail"))["lockID"] + "/pattern-lock/");
                break;
        }

        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        if(res["success"]){
            // Debug.Log(res["data"]);
            // Debug.Log(res["data"]["order"]);
            // Debug.Log(res["data"]["order"][0]);
            bool setStart = false;

            foreach(var order in res["data"]["order"]){
                foreach(var circle in circles){                    
                    if(circle.Value.id == res["data"]["start_pt"] && !setStart){
                        circle.Value.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
                        
                        // set new line
                        lineOnEdit = CreateShadow(circle.Value.transform.localPosition, circle.Value.id);
                        lineOnEditRcTs = lineOnEdit.GetComponent<RectTransform>();
                        circleOnEdit = circle.Value;

                        setStart = true;
                    }
                    
                    else if(circle.Value.id == order.Value){
                        Debug.Log(circle.Value.id);
                        // end previous line
                        lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x, Vector3.Distance(circleOnEdit.transform.localPosition, circle.Value.transform.localPosition));
                        lineOnEditRcTs.rotation = Quaternion.FromToRotation(
                            Vector3.up,
                            (circle.Value.transform.localPosition - circleOnEdit.transform.localPosition).normalized
                        );

                        // set new line
                        lineOnEdit = CreateShadow(circle.Value.transform.localPosition, circle.Value.id);
                        lineOnEditRcTs = lineOnEdit.GetComponent<RectTransform>();
                        circleOnEdit = circle.Value;
                    }
                }
                siblingIdx += 1;
            }
        }
    }


    GameObject CreateShadow(Vector3 pos, int id){
        GameObject line = GameObject.Instantiate(lineObject, canvas.transform);
        line.transform.SetAsFirstSibling();
        
        line.transform.localPosition = new Vector3(pos.x - 25, pos.y, pos.z);

        var lineIdf = line.AddComponent<Circle>();

        lineIdf.id = id;
        
        // lines.Add(lineIdf);

        return line;
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
        line.transform.SetSiblingIndex(siblingIdx);
        
        line.transform.localPosition = new Vector3(pos.x - 25, pos.y, pos.z);
        line.GetComponent<UnityEngine.UI.Image>().color = lineColor;
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
            // circle.Value.GetComponent<UnityEngine.UI.Image>().color = Color.white;
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
        // Debug.Log(lockType);

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
        string locX=PlayerPrefs.GetString("loc_x","Empty");
        string locY=PlayerPrefs.GetString("loc_y","Empty");
        form.AddField("locX", locX); // get loc x
        form.AddField("locY", locY); // get loc y
        form.AddField("groupID", "1");

        UnityWebRequest req = UnityWebRequest.Post(PlatformDefines.apiAddress + "/bomb/create-bomb", form);

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

        SceneManager.LoadScene("MapScene");
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

        yield return req.SendWebRequest();
        // parse the json response
        JSONNode res = JSON.Parse(req.downloadHandler.text);
        if(req.isNetworkError || req.isHttpError){
            Debug.LogError(req.error);
            yield break;
        }
        if(res["success"]){
            Debug.Log(res["data"]);
            SceneManager.LoadScene("MapScene");
            PlayerPrefs.SetString("visiable","n");
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
