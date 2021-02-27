using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // line.transform.SetAsFirstSibling();
        
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
        result.Clear();
        ResetLine();
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
