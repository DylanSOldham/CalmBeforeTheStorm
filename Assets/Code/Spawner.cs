using UnityEngine;

public class Spawner : MonoBehaviour
{
    public WaterController waterController;
    public StormController stormController;

    public GameObject ship;
    public GameObject barrel;
    public GameObject shark;

    private const int MaxResources = 10;
    private const float ResourceSpawnRate = 3;
    private const float ResourceSpawnRadius = 300f;
    private float _resourceTimer = 2;

    private const int MaxEnemies = 10;
    private const float EnemySpawnRate = 3;
    private const float EnemySpawnRadius = 300f;
    private float _enemyTimer = 2;

    // Update is called once per frame
    void Update()
    {
        if (stormController.IsStormActive())
        {
            SpawnEnemies();
        }
        else
        {
            SpawnResources();
        }
    }

    private void SpawnResources()
    {
        if (_resourceTimer <= ResourceSpawnRate)
        {
            _resourceTimer += Time.deltaTime;
            return;
        }
        _resourceTimer = 0;
        SpawnBarrel();
    }

    private void SpawnEnemies()
    {
        if (_enemyTimer <= EnemySpawnRate)
        {
            _enemyTimer += Time.deltaTime;
            return;
        }
        _enemyTimer = 0;
        SpawnShark();
    }

    private void SpawnBarrel()
    {
        var position = GetRandomPointInCircle(ship.transform.position, ResourceSpawnRadius);
        Instantiate(barrel, position, Quaternion.identity);
    }

    private void SpawnShark()
    {
        var position = GetRandomPointInCircle(ship.transform.position, EnemySpawnRadius);
        Instantiate(shark, position, Quaternion.identity);
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