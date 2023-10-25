using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONOFF : MonoBehaviour
{
    Light myLights;
    void Start()
    {
        myLights = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myLights.enabled = !myLights.enabled;

        }
    }
}
