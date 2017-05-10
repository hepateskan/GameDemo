using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCloaking : MonoBehaviour
{
    public Sprite defaultSprite;
    private Timer myTimer;
    // Use this for initialization
    void Start ()
    {
        myTimer = gameObject.GetComponent<Timer>();
        myTimer.setTimer(5f);
        Rotation.toggleCloakStatus();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		if (myTimer.checkTimer())
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            Rotation.toggleCloakStatus();
            Destroy(myTimer);
            Destroy(this);
        }
	}
}
