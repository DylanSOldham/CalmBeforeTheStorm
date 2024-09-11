using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormController : MonoBehaviour
{
    public TextMeshProUGUI state;
    public TextMeshProUGUI countDown;
    public GameObject light;
    public Material Skybox;

    bool stormActive = false;
    float timeUntilChange = 0.0f;

    const float DURATION = 15.0f;

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
            light.SetActive(false);
            RenderSettings.fog = true;
        }
        else
        {
            state.text = "Storm Countdown";
            light.SetActive(true);
            RenderSettings.fog = false;
        }

        int timeRep = (int)timeUntilChange;
        countDown.text = timeRep.ToString();

    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
