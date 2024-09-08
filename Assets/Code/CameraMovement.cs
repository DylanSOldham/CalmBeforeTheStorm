using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform shipTransform;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(shipTransform);
        transform.position = shipTransform.position + new Vector3(0.0f, 3.0f, -10.0f);
    }
}
