using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactble
{
    public Light lighter;
    public bool isOn = false;

    protected override void Interact()
    {
        isOn = !isOn;
        if(isOn)
        {
            lighter.gameObject.SetActive(true);
        }
        else lighter.gameObject.SetActive(false);
    }
}
