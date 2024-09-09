using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Transform shipTransform;
    public WaterController waterController;

    public GameObject directionObject;

    public float moveSpeed = 10f; // Adjust to make it faster/slower
    public float turnSpeed = 50f; // Adjust for faster/slower rotation

    // Start is called before the first frame update
    void Start()
    {
        shipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            shipTransform.position += moveSpeed * -directionObject.transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            shipTransform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            shipTransform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        shipTransform.position = new Vector3(shipTransform.position.x, waterController.GetHeightAtPosition(shipTransform.position), shipTransform.position.z);
    }
}
