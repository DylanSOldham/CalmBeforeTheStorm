using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform shipTransform;
    public Transform shipDirection;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        //cameraTransform.LookAt(shipTransform.position);
        //cameraTransform.position = shipTransform.position + new Vector3(20.0f, 30.0f, 0f);
        Vector3 offset = new Vector3(50, 0, 38); // Adjust these values as needed
        Vector3 shipPosition = shipTransform.position;
        shipPosition.y = 80;
        cameraTransform.position = shipPosition + shipTransform.rotation * offset;

        // Make the camera look at the ship
        Vector3 lookAtPosition = shipTransform.position + -shipDirection.right * 20;
        lookAtPosition.x += 20;
        cameraTransform.LookAt(lookAtPosition);
    }
}