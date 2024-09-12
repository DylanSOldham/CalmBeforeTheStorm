using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public WaterController waterController;

    public GameObject ship;
    public GameObject barrel;

    private const int MaxResources = 10;
    private const float SpawnRate = 3;
    private const float SpawnRadius = 300f;
    private float _timer = 2;

    // Update is called once per frame
    void Update()
    {
        SpawnResources();
        UpdateResources();
    }

    private void UpdateResources()
    {
    }

    private void SpawnResources()
    {
        if (_timer <= SpawnRate)
        {
            _timer += Time.deltaTime;
            return;
        }

        _timer = 0;
        SpawnBarrel();
    }

    private void SpawnBarrel()
    {
        var position = GetRandomPointInCircle(ship.transform.position, SpawnRadius);
        Instantiate(barrel, position, Quaternion.identity);
        Debug.Log(position);
    }
    
    private static Vector3 GetRandomPointInCircle(Vector3 center, float radius)
    {
        // Generate a random angle between 0 and 2π
        var theta = Random.Range(0f, 2f * Mathf.PI);

        // Generate a random distance, scaled by the square root to ensure uniform distribution
        var distance = radius * Mathf.Sqrt(Random.Range(0.35f, 1f));

        // Calculate the coordinates using the angle and distance
        var x = center.x + distance * Mathf.Cos(theta);
        var z = center.z + distance * Mathf.Sin(theta);

        return new Vector3(x, 0, z);
    }
}