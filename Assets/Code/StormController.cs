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
    public float fogStorm = 0.01f;
    public float fogCalm = 0.0001f;

    [SerializeField] private float fogDenstiyRep;

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
            fogDenstiyRep = Mathf.Lerp(RenderSettings.fogDensity, fogStorm, 0.005f);
        }
        else
        {
            state.text = "Storm Countdown";
            fogDenstiyRep = Mathf.Lerp(RenderSettings.fogDensity, fogCalm, 0.005f);
        }

        RenderSettings.fogDensity = fogDenstiyRep;


        int timeRep = (int)timeUntilChange;
        countDown.text = timeRep.ToString();

    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
