using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icebergBehavior : MonoBehaviour
{
    GameObject stormBehavior;
    WaterController waterController;

    private const float sinkHeight = -50.0f;
    private const float floatHeight = -8.0f;
    private float heightAboveWater = sinkHeight;

    // Start is called before the first frame update
    void Start()
    {
        stormBehavior = GameObject.Find("/StormController");
        waterController = GameObject.Find("/Water").GetComponent<WaterController>();
    }

    // Update is called once per frame
    void Update()
    {
        StormController script = stormBehavior.GetComponent<StormController>();
        if (!script.IsStormActive())
        {
            heightAboveWater = Mathf.Lerp(heightAboveWater, sinkHeight, 0.002f);

            if (heightAboveWater < -40.0f)
            {
                Destroy(this.gameObject); 
            }
        }
        else {
            heightAboveWater = Mathf.Lerp(heightAboveWater, floatHeight, 0.01f);
        }

        transform.position = new Vector3(
            transform.position.x, waterController.GetHeightAtPosition(transform.position) + heightAboveWater, transform.position.z
        );
    }
}
