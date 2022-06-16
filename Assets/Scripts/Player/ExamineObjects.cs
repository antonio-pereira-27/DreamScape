using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineObjects : MonoBehaviour
{
    // references
    [SerializeField] private Camera mainCamera;

    private GameObject elegibleObject;

    private Vector3 originalPosition;
    private Vector3 originalRotation;

    [SerializeField]private Transform firstPersonCameraPosition;
    [SerializeField]private Transform thirdPersonCameraPosition;
    
    // variables
    [HideInInspector]public bool examineMode = false;
    private bool elegible;


    // Update is called once per frame
    void Update()
    {
        elegible = gameObject.GetComponent<GrabSystem>().elegible;

        if (elegible && examineMode == false)
            ExamineObject();
        else if (elegible && examineMode)
        {
            InspectObject();
            ExitExamineObject();
        }
    }

    void ExamineObject()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0;

            elegibleObject = gameObject.GetComponent<GrabSystem>().pickedItem.gameObject;
            mainCamera.transform.position = firstPersonCameraPosition.position;

            originalPosition = elegibleObject.transform.position;
            originalRotation = elegibleObject.transform.rotation.eulerAngles;

            elegibleObject.transform.position = mainCamera.transform.position + transform.forward;

            

            examineMode = true;
        }
    }

    void InspectObject()
    {
        if (Input.GetMouseButton(0))
        {
            float rotationSpeed = 15f;

            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;
            
            elegibleObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            elegibleObject.transform.Rotate(Vector3.right, yAxis, Space.World);
        }
    }

    void ExitExamineObject()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mainCamera.transform.position = thirdPersonCameraPosition.position;
            
            elegibleObject.transform.position = originalPosition;
            elegibleObject.transform.eulerAngles = originalRotation;


            Time.timeScale = 1;

            examineMode = false;
        }
    }
}
