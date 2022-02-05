using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

    private Light light;

    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter > 0.75f)
        {
            counter = 0;
            light.enabled = !light.enabled;
        }
    }
}
