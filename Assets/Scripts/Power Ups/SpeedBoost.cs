using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUp
{

    public static Movement playerMovement;

	// Use this for initialization
	void Start ()
    {
		if (playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        }
        this.enabled = false;
	}

    protected override void PickUp()
    {
        playerMovement.speed *= 1.2f;
        base.PickUp();
    }
}
