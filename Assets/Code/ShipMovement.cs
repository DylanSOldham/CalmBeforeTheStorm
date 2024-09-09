using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform shipTransform;
    public WaterController waterController;

    public GameObject directionObject;

    public float moveSpeed = 10f; // Adjust to make it faster/slower
    public float turnFactor = 50f; // Adjust for faster/slower rotation

    private Vector3 _speed = new Vector3(0, 0, 0);
    public Vector3 maxSpeed = new Vector3(5, 0, 5);

    private Vector3 _acceleration = new Vector3(0, 0, 0);
    public Vector3 maxAcceleration = new Vector3(10, 0, 10);

    private float _timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        shipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.W)) {
        //     shipTransform.position += moveSpeed * -directionObject.transform.right * Time.deltaTime;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     shipTransform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        // }
        _timer += Time.deltaTime;
        if (_timer >= 0.2f)
        {
            _timer = 0;
            
            int dir = 0;
            
            if (Input.GetKey(KeyCode.D))
            {
                dir += 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                dir -= 1;
            }

            if (dir != 0)
            {
                if (Mathf.Abs(_acceleration.z) <= maxAcceleration.z)
                {
                    _acceleration.z += 3 * dir;
                    Debug.Log(dir);
                }
                _speed.z += _acceleration.z;
            }
            else
            {
                Debug.Log(dir);
                if (_speed.z > 0)
                {
                    _speed.z -= 2;
                }
                else
                {
                    _speed.z += 2;
                }
            }
            

        }
        var target = shipTransform.position;
        target.y = waterController.GetHeightAtPosition(target);
        shipTransform.position = target;
        shipTransform.Rotate(Vector3.up, _speed.z * Time.deltaTime);
    }
}