using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : PowerUp
{
    public static GameObject[] lights;

    void Start()
    {
        if (lights == null)
        {
            lights = GameObject.FindGameObjectsWithTag("Light");
        }
        this.enabled = false;
    }

    protected override void PickUp()
    {
        foreach(GameObject instance in lights)
        {
            instance.transform.localScale *= 1.25f;
        }
        base.PickUp();
    }
}
