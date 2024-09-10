using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform _shipTransform;
    public WaterController waterController;
    public GameObject openSail;

    private Vector3 _speed = new Vector3(0, 0, 0);
    public Vector3 maxSpeed = new Vector3(20, 0, 20);

    private Vector3 _acceleration = new Vector3(0, 0, 0);
    public Vector3 maxAcceleration = new Vector3(5, 0, 5);

    public Transform cannonOne;
    public Transform cannonTwo;
    public Transform cannonThree;
    public Transform cannonFour;

    public float fireForce = 30f;

    public GameObject cannonBall;

    float shipRotation = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _shipTransform = GetComponent<Transform>();
        openSail = GameObject.Find("sail_open");
    }

    // Update is called once per frame
    void Update()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        if (hAxis >= 0 && _acceleration.z < 0 || hAxis <= 0 && _acceleration.z > 0)
        {
            _acceleration.z = 0;
            _speed.z *= 0.99f * Time.deltaTime;
        } 
            
        if (vAxis >= 0 && _acceleration.x < 0 || vAxis <= 0 && _acceleration.x > 0)
        {
            _acceleration.x = 0;
            _speed.x *= 0.99f * Time.deltaTime;
        } 
        
        openSail.SetActive(vAxis != 0);
            
        _acceleration.z = Math.Clamp(_acceleration.z + 1 * hAxis * Time.deltaTime, -maxAcceleration.z, maxAcceleration.z);
        _speed.z = Math.Clamp(_speed.z + _acceleration.z * Time.deltaTime, -maxSpeed.z, maxSpeed.z);
            
        _acceleration.x = Math.Clamp(_acceleration.x + 1 * vAxis * Time.deltaTime, -maxAcceleration.x, maxAcceleration.x);
        _speed.x = Math.Clamp(_speed.x + _acceleration.x * Time.deltaTime, -maxSpeed.x, maxSpeed.x);
        
        var target = _shipTransform.position;
        target.y = waterController.GetHeightAtPosition(target);
        target -= _shipTransform.right * (_speed.x * Time.deltaTime);
        transform.position = target;

        shipRotation += _speed.z * Time.deltaTime;

        _shipTransform.rotation = Quaternion.identity;
        _shipTransform.Rotate(Vector3.up, shipRotation);
        _shipTransform.rotation *= Quaternion.FromToRotation(Vector3.up, waterController.GetNormalAtPosition(transform.position));

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
}