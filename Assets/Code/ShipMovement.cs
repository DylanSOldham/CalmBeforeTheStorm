using System;
using System.Collections;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public WaterController waterController;
    public Transform sailTransform;

    float shipForwardVelocity = 0.0f;
    const float shipForwardAcceleration = 0.001f;
    const float maxShipForwardVeloicty = 0.45f;
    private bool sailContracted = true;
    private bool sailMoving = false;

    float shipRotation = 0.0f;
    float shipAngularVelocity = 0.0f;
    const float shipAngularAcceleration = 0.01f;
    const float maxShipAngularVelocity = 0.75f;

    public float WaitBetweenShots = 2f;
    public float timeBetweenShots = 0;

    public Transform cannonOne;
    public Transform cannonTwo;
    public Transform cannonThree;
    public Transform cannonFour;

    public float fireForce = 30f;

    public GameObject cannonBall;

    public GameObject particleEffectCannon;

    public Transform orangeBar;

    // Start is called before the first frame update
    void Start()
    {
        sailTransform = GameObject.Find("sail_open").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveShip();
        UpdateSail();

        timeBetweenShots += Time.deltaTime;

        changeBar();

        //work on angle cannon shoots at
        if (Input.GetMouseButtonDown(0) && (timeBetweenShots >= WaitBetweenShots))
        {
            timeBetweenShots = 0f;

            //shoot cannonOne
            Vector3 offset = cannonOne.position + new Vector3(0, 0, 2f);
            GameObject cannonBallOne = Instantiate(cannonBall, offset, cannonOne.rotation);
            GameObject particleEffect = Instantiate(particleEffectCannon, offset, cannonOne.rotation);
            Rigidbody cannonBallRigidBody = cannonBallOne.GetComponent<Rigidbody>();
            cannonBallRigidBody.velocity = Vector3.forward * fireForce;
            Destroy(particleEffect, 1f);

            //shoot cannonTwo
            Vector3 offset2 = cannonTwo.position + new Vector3(0, 0, 2f);
            GameObject cannonBallTwo = Instantiate(cannonBall, offset2, cannonTwo.rotation);
            Rigidbody cannonBallRigidBody2 = cannonBallTwo.GetComponent<Rigidbody>();
            cannonBallRigidBody2.velocity = Vector3.forward * fireForce;
            GameObject particleEffect2 = Instantiate(particleEffectCannon, offset2, cannonTwo.rotation);
            Destroy(particleEffect2, 1f);

            //shoot cannonThree
            Vector3 offset3 = cannonThree.position + new Vector3(0, 0, -2f);
            GameObject cannonBallThree = Instantiate(cannonBall, offset3, cannonThree.rotation);
            Rigidbody cannonBallRigidBody3 = cannonBallThree.GetComponent<Rigidbody>();
            cannonBallRigidBody3.velocity = -Vector3.forward * fireForce;
            GameObject particleEffect3 = Instantiate(particleEffectCannon, offset3, cannonThree.rotation);
            Destroy(particleEffect3, 1f);

            //shoot cannonFour
            Vector3 offset4 = cannonFour.position + new Vector3(0, 0, -2f);
            GameObject cannonBallFour = Instantiate(cannonBall, offset4, cannonFour.rotation);
            Rigidbody cannonBallRigidBody4 = cannonBallFour.GetComponent<Rigidbody>();
            cannonBallRigidBody4.velocity = -Vector3.forward * fireForce;
            GameObject particleEffect4 = Instantiate(particleEffectCannon, offset4, cannonFour.rotation);
            Destroy(particleEffect4, 1f);
        }
    }
    
    private void UpdateSail() // Called in FixedUpdate
    {
        if (sailMoving) return;
        if (shipForwardVelocity >= 0.1f && sailContracted)
        {
            StartCoroutine(MoveSail(true));
        }
            
        if (shipForwardVelocity <= 0.1f && !sailContracted)
        {
            StartCoroutine(MoveSail(false));
        }
    }

    private IEnumerator MoveSail(bool extend) // true => Extend Sail; false => Retract Sail;
    {
        sailMoving = true;
        for (float t = 0; t < 1; t += Time.deltaTime / 1.5f)
        {
            // Interpolate the Y scale
            var originalScale = extend ? new Vector3(1, 0, 1) : Vector3.one;
            var targetScale = extend ? Vector3.one : new Vector3(1, 0, 1);
            var newScale = Vector3.Lerp(originalScale, targetScale, t);
            sailTransform.localScale = newScale;
            
            // Interpolate the local Y position
            var originalY = extend ? 13f : 10.54397f;
            var targetY = extend ? 10.54397f : 13f;
            var newY = Mathf.Lerp(originalY, targetY, t);
            var newPosition = new Vector3(sailTransform.localPosition.x, newY, sailTransform.localPosition.z);
            sailTransform.localPosition = newPosition;
            yield return null;
        }
        sailContracted = !extend;
        sailMoving = false;
    }

    // Call in FixedUpdate
    void MoveShip()
    {
        // Collect Player Input
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        // Enact Forward Acceleration
        var new_position = transform.position;
        shipForwardVelocity = Math.Clamp(shipForwardVelocity + vAxis * shipForwardAcceleration, -maxShipForwardVeloicty,
            maxShipForwardVeloicty);
        new_position -= shipForwardVelocity * transform.right;
        new_position.y = waterController.GetHeightAtPosition(new_position);
        transform.position = new_position;
        new_position.y = 0.0f;
        waterController.transform.position = new_position;

        // Enact Angular Acceleration
        shipAngularVelocity = Math.Clamp(shipAngularVelocity + hAxis * shipAngularAcceleration, -maxShipAngularVelocity,
            maxShipAngularVelocity);
        shipRotation += shipAngularVelocity;

        // Drag
        if (vAxis.Equals(0))
            shipForwardVelocity *= 0.98f;
        if (hAxis.Equals(0))
            shipAngularVelocity *= 0.98f;

        // Orient ship along the plane of the water surface
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up, shipRotation);
        transform.rotation *=
            Quaternion.FromToRotation(Vector3.up, waterController.GetNormalAtPosition(transform.position));
    }

    public void changeBar()
    {
        float percent = timeBetweenShots / WaitBetweenShots;

        // Clamp percent between 0 and 1 to avoid invalid scales
        percent = Mathf.Clamp01(percent);

        //Debug.Log($"{percent}");

        if (percent >= 1)
        {
            orangeBar.localScale = new Vector3(1, orangeBar.localScale.y, orangeBar.localScale.z);
        }
        else
        {
            orangeBar.localScale = new Vector3(percent, orangeBar.localScale.y, orangeBar.localScale.z);
        }
    }
}