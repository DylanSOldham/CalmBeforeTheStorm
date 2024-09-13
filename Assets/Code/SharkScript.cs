using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkScript : MonoBehaviour
{
    public WaterController waterController;
    public StormController stormController;
    public Transform shipTransform;

    private float _yOffset;
    private const float RiseDuration = 3.0f;
    
    private float _randomSpeedFactor = 4.0f;
    private Vector3 _randomDirectionOffset;
    private const float BaseSpeed = 10.0f;
    private const float DirectionChangeInterval = 2.0f;
    private float _timeSinceLastDirectionChange;

    private void Start()
    {
        StartCoroutine(RiseAnimation());
        waterController = GameObject.Find("/Water").GetComponent<WaterController>();
        shipTransform = GameObject.Find("/Ship").transform;
        stormController = GameObject.Find("/StormController").GetComponent<StormController>();

        // Start with a random offset for the direction
        _randomDirectionOffset = Random.insideUnitSphere * 0.2f;
        _randomDirectionOffset.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y = waterController.GetHeightAtPosition(newPosition) - _yOffset - 1;

        // Calculate direction towards the ship and apply random offset
        var directionToShip = (shipTransform.position - transform.position).normalized + _randomDirectionOffset;

        // Randomize speed slightly
        var speed = BaseSpeed * _randomSpeedFactor;
        
        newPosition += directionToShip * (speed * Time.deltaTime);

        transform.position = newPosition;

        // Make the shark face the ship
        var targetRotation = Quaternion.LookRotation(directionToShip);
        targetRotation *= Quaternion.Euler(0, 90, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

        // Randomly adjust direction and speed at intervals
        _timeSinceLastDirectionChange += Time.deltaTime;
        if (_timeSinceLastDirectionChange >= DirectionChangeInterval)
        {
            ChangeDirectionAndSpeed();
            _timeSinceLastDirectionChange = 0.0f;
        }
        
        if (!stormController.IsStormActive()) 
        {
            StartCoroutine(SinkAnimation());
        }
    }

    private void ChangeDirectionAndSpeed()
    {
        _randomDirectionOffset = Random.insideUnitSphere * 0.2f;
        _randomDirectionOffset.y = 0;

        _randomSpeedFactor = Random.Range(0.8f, 1.2f);
    }

    private IEnumerator RiseAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / RiseDuration)
        {
            _yOffset = Mathf.Lerp(20, 0, t);
            yield return null;
        }
    }

    private IEnumerator SinkAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / RiseDuration)
        {
            _yOffset = Mathf.Lerp(0, 20, t);
            yield return null;
        }
        
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CannonBall"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
            Debug.Log("Die, you bastard!");
        }
    }
}
