using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform shipTransform;
    public WaterController waterController;

    public GameObject directionObject;

    private Vector3 _speed = new Vector3(0, 0, 0);
    public Vector3 maxSpeed = new Vector3(15, 0, 15);

    private Vector3 _acceleration = new Vector3(0, 0, 0);
    public Vector3 maxAcceleration = new Vector3(5, 0, 5);

    private float _timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        shipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.2f)
        {
            _timer = 0;

            var dir = 0;

            if (Input.GetKey(KeyCode.D))
            {
                dir += 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                dir -= 1;
            }

            // Auto stay still
            if (dir == 0)
            {
                _acceleration.z = 0;
                if (_speed.z > 0)
                {
                    var i = _speed.z - 3;
                    _speed.z = i < 0 ? 0 : i;
                }
                else
                {
                    var i = _speed.z + 3;
                    _speed.z = i > 0 ? 0 : i;
                }
            }

            if (dir == 1 && _acceleration.z < 0)
            {
                _acceleration.z = 0;
            }

            if (dir == -1 && _acceleration.z > 0)
            {
                _acceleration.z = 0;
            }

            if (Mathf.Abs(_acceleration.z) <= maxAcceleration.z)
            {
                var i = _acceleration.z + 1 * dir;
                _acceleration.z = Mathf.Abs(i) > maxAcceleration.z ? maxAcceleration.z * dir : i;
            }

            var j = _speed.z + _acceleration.z * 2;
            _speed.z = Mathf.Abs(j) > maxSpeed.z ? Mathf.Sign(j) * maxSpeed.z : j;
            
            print(_speed.z);
            print(_acceleration.z);

            if (Input.GetKey(KeyCode.W))
            {
                _acceleration.x += 1;
            }
        }

        var target = shipTransform.position;
        target.y = waterController.GetHeightAtPosition(target);
        shipTransform.position = target;
        shipTransform.Rotate(Vector3.up, _speed.z * Time.deltaTime);
    }
}