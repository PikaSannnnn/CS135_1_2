using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    private bool lightEnabled = false;

    private Light GetLightChild()
    {
        return this.GetComponentInChildren<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Light light = GetLightChild();
        light.intensity = 0;
    }

    public bool isLightEnabled()
    {
        return lightEnabled;
    }

    public void Toggle(bool b)
    {
        Light light = GetLightChild();
        if (b)
        {
            light.intensity = 0;
        }
        else
        {
            light.intensity = 100;
        }
        lightEnabled = b;
    }

    public void Toggle()
    {
        Light light = GetLightChild();
        if (lightEnabled)
        {
            light.intensity = 0;
        }
        else
        {
            light.intensity = 100;
        }
        lightEnabled = !lightEnabled;
    }

    void OnTriggerStay(Collider other)
    {
        this.GetComponentInParent<LazerGame>().TouchingLight(this);
    }
}
