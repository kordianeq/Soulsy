using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
