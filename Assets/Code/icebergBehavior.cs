using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icebergBehavior : MonoBehaviour
{
    GameObject stormBehavior;

    // Start is called before the first frame update
    void Start()
    {
        stormBehavior = GameObject.Find("/StormController");
    }

    // Update is called once per frame
    void Update()
    {
        StormController script = stormBehavior.GetComponent<StormController>();
        if(!script.stormActive){
            Destroy(this.gameObject);
        }
    }
}
