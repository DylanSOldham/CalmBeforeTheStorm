using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public WaterController waterController;

    float shipForwardVelocity = 0.0f;
    const float shipForwardAcceleration = 0.001f;
    const float maxShipForwardVeloicty = 0.45f;

    float shipRotation = 0.0f;
    float shipAngularVelocity = 0.0f;
    const float shipAngularAcceleration = 0.01f;
    const float maxShipAngularVelocity = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Collect Player Input
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");
        
        // Enact Forward Acceleration
        var new_position = transform.position;
        shipForwardVelocity = Math.Clamp(shipForwardVelocity + vAxis * shipForwardAcceleration, -maxShipForwardVeloicty, maxShipForwardVeloicty);
        new_position -= shipForwardVelocity * transform.right;
        new_position.y = waterController.GetHeightAtPosition(new_position);
        transform.position = new_position;

        // Enact Angular Acceleration
        shipAngularVelocity = Math.Clamp(shipAngularVelocity + hAxis * shipAngularAcceleration, -maxShipAngularVelocity, maxShipAngularVelocity);
        shipRotation += shipAngularVelocity;

        // Drag
        if (vAxis.Equals(0))
            shipForwardVelocity *= 0.98f;
        if (hAxis.Equals(0))
            shipAngularVelocity *= 0.98f;

        // Orient ship along the plane of the water surface
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up, shipRotation);
        transform.rotation *= Quaternion.FromToRotation(Vector3.up, waterController.GetNormalAtPosition(transform.position));
    }
}