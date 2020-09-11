using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// unity plugin DoTween, handle the page transition
using DG.Tweening;


public class PageSwiper : MonoBehaviour
{
    // variable of panel handler
    public Transform panelHandler;

    // start page
    public int startPage;
    public int leftPages;
    public int rightPages;

    // correction factor
    public float correctionFactor;

    // Start is called before the first frame update
    void Start()
    {
        // init its anchor
        panelHandler.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // initialize the panels position
        for (int idx = 0; idx < panelHandler.childCount; idx++)
        {
            float preWidth = 0;

            // initialize GameObject PanelHandler position
            if (idx == 0)
            {
                // set the first panel position
                panelHandler.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                // set all the panel position instead of first one
                preWidth = .5f * (panelHandler.GetChild(idx - 1).GetComponent<RectTransform>().rect.width + panelHandler.GetChild(idx).GetComponent<RectTransform>().rect.width);

                panelHandler.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = new Vector2(idx * preWidth, 0);
            }
        }

    }

    // Update is called once per frame
    bool holdDown = false;
    float startPosX;
    float runtimePos;
    float deltaX;

    // middle call 0, right decrease left increase
    public int pageIdx = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdDown = true;
            startPosX = Input.mousePosition.x;
            runtimePos = panelHandler.GetComponent<RectTransform>().anchoredPosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            holdDown = false;
            float width = panelHandler.GetChild(0).GetComponent<RectTransform>().rect.width;
            if (Mathf.Abs(deltaX) >= (width / 2) || Mathf.Abs(Input.GetAxis("Mouse X")) >= 3)
            {
                if (deltaX > correctionFactor)
                {
                    // to right
                    pageIdx++;
                    if (pageIdx > leftPages)
                        pageIdx = leftPages;
                }
                else
                {
                    // to left
                    pageIdx--;
                    if (pageIdx < -rightPages)
                        pageIdx = -rightPages;
                }
                panelHandler.GetComponent<RectTransform>().DOAnchorPosX(pageIdx * width, 0.5f);
            }
            else
            {
                panelHandler.GetComponent<RectTransform>().DOAnchorPosX(pageIdx * width, 0.5f);
            }
        }
        if (holdDown)
        {
            deltaX = (Input.mousePosition.x - startPosX) * correctionFactor / 10;
            panelHandler.GetComponent<RectTransform>().anchoredPosition = new Vector2(deltaX * correctionFactor / 50 + runtimePos, 0);
        }
    }


}