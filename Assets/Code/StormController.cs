using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormController : MonoBehaviour
{
    bool stormActive = false;
    float timeUntilChange = 0.0f;

    const float DURATION = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeUntilChange -= Time.fixedDeltaTime;
        if (timeUntilChange < 0) {
            stormActive = !stormActive;
            timeUntilChange = DURATION;
        }
    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
