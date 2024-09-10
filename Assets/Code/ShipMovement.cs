using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public WaterController waterController;
    public GameObject openSail;

    float shipForwardVelocity = 0.0f;
    const float shipForwardAcceleration = 0.001f;
    const float maxShipForwardVeloicty = 0.45f;

    float shipRotation = 0.0f;
    float shipAngularVelocity = 0.0f;
    const float shipAngularAcceleration = 0.01f;
    const float maxShipAngularVelocity = 0.75f;

    public Transform cannonOne;
    public Transform cannonTwo;
    public Transform cannonThree;
    public Transform cannonFour;

    public float fireForce = 30f;

    public GameObject cannonBall;

    // Start is called before the first frame update
    void Start()
    {
        openSail = GameObject.Find("sail_open");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveShip();

        if(Input.GetMouseButtonDown(0)){
            //shoot cannonOne
            Vector3 offset = cannonOne.position + new Vector3(0,0,2f);
            GameObject cannonBallOne = Instantiate(cannonBall, offset, cannonOne.rotation);
            Rigidbody cannonBallRigidBody = cannonBallOne.GetComponent<Rigidbody>();
            cannonBallRigidBody.velocity = Vector3.forward * fireForce;

            //shoot cannonTwo
            Vector3 offset2 = cannonTwo.position + new Vector3(0,0,2f);
            GameObject cannonBallTwo = Instantiate(cannonBall, offset2, cannonTwo.rotation);
            Rigidbody cannonBallRigidBody2 = cannonBallTwo.GetComponent<Rigidbody>();
            cannonBallRigidBody2.velocity = Vector3.forward * fireForce;

            //shoot cannonThree
            Vector3 offset3 = cannonThree.position + new Vector3(0,0,-2f);
            GameObject cannonBallThree = Instantiate(cannonBall, offset3, cannonThree.rotation);
            Rigidbody cannonBallRigidBody3 = cannonBallThree.GetComponent<Rigidbody>();
            cannonBallRigidBody3.velocity = -Vector3.forward * fireForce;

            //shoot cannonFour
            Vector3 offset4 = cannonFour.position + new Vector3(0,0,-2f);
            GameObject cannonBallFour = Instantiate(cannonBall, offset4, cannonFour.rotation);
            Rigidbody cannonBallRigidBody4 = cannonBallFour.GetComponent<Rigidbody>();
            cannonBallRigidBody4.velocity = -Vector3.forward * fireForce;
        }
    }

    // Call in FixedUpdate
    void MoveShip()
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
        new_position.y = 0.0f;
        waterController.transform.position = new_position;

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