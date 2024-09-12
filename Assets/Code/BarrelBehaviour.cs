using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public WaterController waterController;
    
    private float _yOffset;
    private const float RiseDuration = 3.0f;

    private void Start()
    {
        StartCoroutine(RiseAnimation());
        waterController = GameObject.Find("/Water").GetComponent<WaterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y = waterController.GetHeightAtPosition(newPosition) - _yOffset - 1;
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
