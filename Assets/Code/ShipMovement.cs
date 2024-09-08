using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform shipTransform;

    // Start is called before the first frame update
    void Start()
    {
        shipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            shipTransform.position += 0.1f * Vector3.forward;
        }
    }
}
