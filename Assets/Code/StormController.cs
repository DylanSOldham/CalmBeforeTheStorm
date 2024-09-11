using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormController : MonoBehaviour
{
    public TextMeshProUGUI state;
    public TextMeshProUGUI countDown;
    

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


        if(stormActive){
            state.text = "Survive";

        }else{
            state.text = "Storm Countdown";

        }

        int timeRep = (int)timeUntilChange;
        countDown.text = timeRep.ToString();

    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
