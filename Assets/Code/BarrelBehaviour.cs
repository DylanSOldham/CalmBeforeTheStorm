using System.Collections;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public WaterController waterController;
    
    private float _yOffset;
    private const float RiseDuration = 3.0f;
    private const float BobbingSpeed = 1.0f;
    private const float BobbingAmplitude = 0.5f;
    private Vector3 _driftOffset;
    private const float DriftSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(RiseAnimation());
        waterController = GameObject.Find("/Water").GetComponent<WaterController>();

        // Initialize a random drift direction
        _driftOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * DriftSpeed;
    }

    void Update()
    {
        var newPosition = transform.position;
        
        var bobbingEffect = Mathf.Sin(Time.time * BobbingSpeed) * BobbingAmplitude;

        newPosition.y = waterController.GetHeightAtPosition(newPosition) - _yOffset + bobbingEffect;

        newPosition += _driftOffset * Time.deltaTime;

        transform.position = newPosition;
    }

    private IEnumerator RiseAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / RiseDuration)
        {
            _yOffset = Mathf.Lerp(20, 0, t);
            yield return null;
        }
    }
}