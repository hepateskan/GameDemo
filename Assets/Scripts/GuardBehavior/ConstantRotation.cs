using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class rotates a guard at a constant rate. When it is finished, it transitions back to patrol.
*/

public class ConstantRotation : MonoBehaviour
{

    public Timer timeToRotate;
    public GuardBehavior parentGuard;

    void Awake()
    {
        timeToRotate = gameObject.AddComponent<Timer>();
        enabled = false;
    }

	// Update is called once per frame
	void LateUpdate ()
    {
        if (!timeToRotate.checkTimer())
        {
            transform.Rotate(transform.forward, 180f * Time.smoothDeltaTime);
        }
        else
        {
            parentGuard.setState(GuardBehavior.guardState.Patrol);
            enabled = false;
        }
    }
}
