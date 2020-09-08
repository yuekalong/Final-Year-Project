using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{

    public RectTransform rectTransform;

    private void Awake()
    {
        // get the attached game object RectTransform
        rectTransform = GetComponent<RectTransform>();
        RefreshPanel(Screen.safeArea);
    }

    // when enable
    private void OnEnable()
    {
        SafeAreaDetection.OnSafeAreaChanged += RefreshPanel;
    }

    // when disable
    private void OnDisable()
    {
        SafeAreaDetection.OnSafeAreaChanged -= RefreshPanel;
    }

    private void RefreshPanel(Rect safeArea)
    {
        // set the anchor of the area
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        // divide the anchor by its screen width and height
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}