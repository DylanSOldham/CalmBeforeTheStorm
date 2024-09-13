using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBallBehavior : MonoBehaviour
{

    public float timerAlive = 2f;
    public float timeTracker = 0f;

    // Update is called once per frame
    void Update()
    {
        timeTracker += Time.deltaTime;
        if(timeTracker >= timerAlive)
        {
            Destroy(this.gameObject);
        }
    }
    
}
