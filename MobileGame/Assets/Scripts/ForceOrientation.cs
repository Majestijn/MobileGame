using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOrientation : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(Screen.width + "      " + Screen.height);
        }
    }
}
