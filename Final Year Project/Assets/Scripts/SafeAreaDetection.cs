using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaDetection : MonoBehaviour
{

    // delegate like pointer in C
    public delegate void SafeAreaChanged(Rect safeArea);
    public static event SafeAreaChanged OnSafeAreaChanged;

    private Rect safeArea;

    private void Awake()
    {
        // set area into screen safe area
        safeArea = Screen.safeArea;
    }

    private void Update()
    {
        // if safe area is not safe, set back
        if (safeArea != Screen.safeArea)
        {
            safeArea = Screen.safeArea;
            OnSafeAreaChanged?.Invoke(safeArea);
        }
    }
}