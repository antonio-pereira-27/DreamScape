using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // References
    [SerializeField] private Transform playerBody;

    [SerializeField] private Transform playerHead;

    // Variables
     public float mouseSensitivity = 2.0f;
     public float clampAngle = 30.0f;
 
     private float rotY = 0.0f; // rotation around the up/y axis
     private float rotX = 0.0f; // rotation around the right/x axis

     private Quaternion rotation;
 
     void Start ()
     {
         Cursor.lockState = CursorLockMode.Locked;
         Vector3 rot = transform.localRotation.eulerAngles;
         rotY = rot.y;
         rotX = rot.x;
     }
 
     void Update ()
     {
         float mouseX = Input.GetAxis("Mouse X");
         float mouseY = -Input.GetAxis("Mouse Y");
 
         rotY += mouseX * mouseSensitivity;
         rotX += mouseY * mouseSensitivity;


         rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
         
         Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

         transform.rotation = localRotation;
         
         playerBody.Rotate(Vector3.up * mouseX * mouseSensitivity);
         playerHead.Rotate(Vector3.right * mouseY * mouseSensitivity);
         rotation = localRotation;
     }

     public Quaternion GetRotation()
     {
         return rotation;
     }
    
}
