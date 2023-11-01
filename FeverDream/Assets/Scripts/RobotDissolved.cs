using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDissolved : MonoBehaviour
{
    public Material dissolveMat;
    public float dissolvement;
    public float maxDissolvement;

    void Start() 
    {
        dissolveMat.SetFloat("_FadeIn", dissolvement / maxDissolvement);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            dissolvement -= 5;
            dissolveMat.SetFloat("_FadeIn", dissolvement / maxDissolvement);
        }
    }
}
