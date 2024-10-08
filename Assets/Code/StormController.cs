using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormController : MonoBehaviour
{
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    public TextMeshProUGUI state;
    public TextMeshProUGUI countDown;
    public Transform ship;
    public Light light;

    Transform cameraTransform;
    ParticleSystem rainParticleSystem;

    public GameObject iceBergPrefab;
    public float timeBetweenIcebergSpawns = 1f;
    public float IcebergTimer = 0;

    public float fogStorm = 0.01f;
    public float fogCalm = 0.0001f;

    public float lightStorm = 0.1f;
    public float lightCalm = 0.0001f;

    public float stormTransitionState = 0.0f;

    [SerializeField] private float fogDenstiyRep;

    private bool stormActive = false;
    float timeUntilChange = 0.0f;

    const float DURATION = 30.0f;

    public int rounds = 1;

    public TextMeshProUGUI roundsText;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
        rainParticleSystem = GetComponent<ParticleSystem>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];
        audioSource3 = audioSources[2];

        // Assign audio clips (or set them in the Inspector)
        audioSource1.clip = clip1;
        audioSource2.clip = clip2;
        audioSource3.clip = clip3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeUntilChange -= Time.fixedDeltaTime;
        IcebergTimer += Time.fixedDeltaTime;

        if (timeUntilChange < 0) {
            if(stormActive == true)
            {
                rounds++;
                roundsText.text = rounds.ToString();
                audioSource1.Stop();
                audioSource2.Stop();
                audioSource3.Play();
            }else{
                audioSource1.Play();
                audioSource2.Play();
                audioSource3.Stop();
            }
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
                float randomNum = Random.Range(125f, 200f);
                float randomNum2 = Random.Range(-125f,125f);

                Vector3 distanceOffset = (-ship.right * randomNum);
                Vector3 xOffset = ship.forward * (randomNum2);
                Vector3 inFrontOfShip = ship.position + distanceOffset + xOffset;

                GameObject iceBergInstance = Instantiate(iceBergPrefab, inFrontOfShip, Quaternion.identity);
                
            }
            
            rainParticleSystem.Play();
        }
        else
        {
            state.text = "Storm Countdown";
            stormTransitionState = Mathf.Lerp(stormTransitionState, 0.0f, 0.005f);
            rainParticleSystem.Stop();
        }

        RenderSettings.fogDensity = fogCalm + stormTransitionState * (fogStorm - fogCalm);
        RenderSettings.skybox.SetFloat("_Exposure", 0.5f - stormTransitionState);

        light.intensity = 1.0f - 1.5f * stormTransitionState;

        int timeRep = (int)timeUntilChange;
        countDown.text = timeRep.ToString();

        transform.position = cameraTransform.position + 50.0f * Vector3.up;
    }

    public bool IsStormActive()
    {
        return stormActive;
    }
}
