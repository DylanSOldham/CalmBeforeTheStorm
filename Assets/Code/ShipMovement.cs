using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform _shipTransform;
    public WaterController waterController;

    private Vector3 _speed = new Vector3(0, 0, 0);
    public Vector3 maxSpeed = new Vector3(15, 0, 15);

    private Vector3 _acceleration = new Vector3(0, 0, 0);
    public Vector3 maxAcceleration = new Vector3(5, 0, 5);
    
    // Start is called before the first frame update
    void Start()
    {
        _shipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");
        
        if (hAxis >= 0 && _acceleration.z < 0 || hAxis <= 0 && _acceleration.z > 0)
        {
            _acceleration.z = 0;
            _speed.z *= 0.75f * Time.deltaTime;
        } 
            
        if (vAxis >= 0 && _acceleration.x < 0 || vAxis <= 0 && _acceleration.x > 0)
        {
            _acceleration.x = 0;
            _speed.x *= 0.75f * Time.deltaTime;
        } 
            
        _acceleration.z = Math.Clamp(_acceleration.z + 1 * hAxis * Time.deltaTime, -maxAcceleration.z, maxAcceleration.z);
        _speed.z = Math.Clamp(_speed.z + _acceleration.z * Time.deltaTime, -maxSpeed.z, maxSpeed.z);
            
        _acceleration.x = Math.Clamp(_acceleration.x + 1 * vAxis * Time.deltaTime, -maxAcceleration.x, maxAcceleration.x);
        _speed.x = Math.Clamp(_speed.x + _acceleration.x * Time.deltaTime, -maxSpeed.x, maxSpeed.x);
        
        var target = _shipTransform.position;
        target.y = waterController.GetHeightAtPosition(target);
        target -= _shipTransform.right * (_speed.x * Time.deltaTime);
        transform.position = target;
        _shipTransform.Rotate(Vector3.up, _speed.z * Time.deltaTime);
    }
}