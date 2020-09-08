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

    // Start is called before the first frame update
    void Start()
    {

        // initialize the panels position
        for (int idx = 0; idx < panelHandler.childCount; idx++)
        {
            // initialize GameObject PanelHandler position
            if (idx.Equals(0))
            {
                // set its anchor
                panelHandler.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // set the first panel position
                panelHandler.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                // set all the panel position instead of first one
                float PreWidth = .5f * (panelHandler.GetChild(idx - 1).GetComponent<RectTransform>().rect.width + panelHandler.GetChild(idx).GetComponent<RectTransform>().rect.width);

                panelHandler.GetChild(idx).GetComponent<RectTransform>().anchoredPosition = new Vector2(idx * PreWidth, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        OnSliderPage();
    }

    bool isDown = false;
    float mousePosX;
    float runtimePos;
    float deltaX;
    int sliderIndex = 0;
    void OnSliderPage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDown = true;
            mousePosX = Input.mousePosition.x;
            runtimePos = panelHandler.GetComponent<RectTransform>().anchoredPosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
            float width = panelHandler.GetChild(0).GetComponent<RectTransform>().rect.width;
            if (Mathf.Abs(deltaX) >= (width / 2) || Mathf.Abs(Input.GetAxis("Mouse X")) >= 3)
            {
                if (deltaX > 0)
                {
                    // to right
                    sliderIndex++;
                    if (sliderIndex > 0)
                        sliderIndex = 0;
                }
                else
                {
                    // to left
                    sliderIndex--;
                    if (sliderIndex < (-panelHandler.childCount + 1))
                        sliderIndex = -panelHandler.childCount + 1;
                }
                panelHandler.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
            }
            else
            {
                panelHandler.GetComponent<RectTransform>().DOAnchorPosX(sliderIndex * width, 0.5f);
            }
        }
        if (isDown)
        {
            deltaX = Input.mousePosition.x - mousePosX;
            panelHandler.GetComponent<RectTransform>().anchoredPosition = new Vector2(deltaX + runtimePos, 0);
        }
    }
}