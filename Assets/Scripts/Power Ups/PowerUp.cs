using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        this.enabled = false;
	}
	
    void Update()
    {
        if (Input.GetButtonDown("PickUp"))
        {
            PickUp();
        }
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (collidingObject.gameObject.tag == "Player")
        {
            this.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collidingObject)
    {
        if (collidingObject.gameObject.tag == "Player")
        {
            this.enabled = false;
        }
    }

    protected virtual void PickUp()
    {
        Destroy(gameObject);
    }
}
