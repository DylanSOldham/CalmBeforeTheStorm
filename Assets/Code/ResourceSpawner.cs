using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public WaterController waterController;
    
    public GameObject ship;
    public GameObject barrel;
    private List<GameObject> _resources;

    private const int MaxResources = 10;
    private const float SpawnRate = 10;
    private float _timer = 9;

    // Start is called before the first frame update
    void Start()
    {
        _resources = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnResources();
        UpdateResources();
    }

    private void UpdateResources()
    {
        foreach (var resource in _resources)
        {
            // Orient the resource along the plane of the water surface
            // resource.transform.rotation = Quaternion.identity;
            // resource.transform.Rotate(Vector3.up, );
            var newPosition = resource.transform.position;
            newPosition.y = waterController.GetHeightAtPosition(newPosition);
            resource.transform.position = newPosition;
            Debug.Log("aaaaaa aabdawaw barreeeel");
        }
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
        _resources.Add(Instantiate(barrel, ship.transform.position, Quaternion.identity));
    }
    
}