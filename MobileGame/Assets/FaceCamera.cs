using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, Vector3.up);
        lookRotation.y = 0f;
        lookRotation.z = 0f;
        lookRotation.w = 0f;

        transform.rotation = lookRotation;
    }
}
