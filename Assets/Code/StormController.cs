using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormController : MonoBehaviour
{
    public TextMeshProUGUI state;
    public TextMeshProUGUI countDown;
    public Material skybox;
    public Transform ship;
    public Light light;

    public GameObject iceBergPrefab;
    public float timeBetweenIcebergSpawns = 1f;
    public float IcebergTimer = 0;

    public float fogStorm = 0.01f;
    public float fogCalm = 0.0001f;

    public float lightStorm = 0.1f;
    public float lightCalm = 0.0001f;

    public float stormTransitionState = 0.0f;

    [SerializeField] private float fogDenstiyRep;

    public bool stormActive = false;
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
        IcebergTimer += Time.fixedDeltaTime;

        if (timeUntilChange < 0) {
            stormActive = !stormActive;
            timeUntilChange = DURATION;
        }

        if (stormActive)
        {
            state.text = "Survive";
            stormTransitionState = Mathf.Lerp(stormTransitionState, 1.0f, 0.005f);

            if (IcebergTimer >= timeBetweenIcebergSpawns)
            {
                IcebergTimer = 0f;
                float randomNum = Random.Range(150f, 300f);
                Vector3 inFrontOfShip = ship.position + (-ship.right * randomNum);
                GameObject iceBergInstance = Instantiate(iceBergPrefab, inFrontOfShip, Quaternion.identity);
            }
        }
        else
        {
            state.text = "Storm Countdown";
            stormTransitionState = Mathf.Lerp(stormTransitionState, 0.0f, 0.005f);
        }

        RenderSettings.fogDensity = fogCalm + stormTransitionState * (fogStorm - fogCalm);
        skybox.SetFloat("_Exposure", 0.5f - stormTransitionState);

        light.intensity = 1.0f - 1.5f * stormTransitionState;

        int timeRep = (int)timeUntilChange;
        countDown.text = timeRep.ToString();
    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
