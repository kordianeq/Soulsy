using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float rotationOnX;
    float rotationOnY;
    [SerializeField] float mouseSensitivity = 150f;
    [SerializeField] Transform cameraPoint;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

        rotationOnX -= mouseY;
        

        rotationOnY += mouseX;
        rotationOnX = Mathf.Clamp(rotationOnX, -30f, 40f);

        cameraPoint.rotation = Quaternion.Euler(rotationOnX, rotationOnY, 0);
        
        
        
    }
}
